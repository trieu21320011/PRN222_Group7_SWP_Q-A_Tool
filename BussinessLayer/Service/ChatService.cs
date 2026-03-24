using AutoMapper;
using BussinessLayer.IServices;
using BussinessLayer.ViewModels.ChatDTOs;
using DataLayer.DataLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Service
{
    public class ChatService : IChatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SWP391QAContext _context;

        public ChatService(IUnitOfWork unitOfWork, SWP391QAContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        // Generate a consistent room name for direct chats
        private string GetDirectChatRoomName(int userId1, int userId2)
        {
            var minId = Math.Min(userId1, userId2);
            var maxId = Math.Max(userId1, userId2);
            return $"Direct_{minId}_{maxId}";
        }

        public async Task<ChatRoomDTO?> GetChatRoomByIdAsync(int chatRoomId, int currentUserId)
        {
            var room = await _context.ChatRooms
                .Include(r => r.CreatedBy)
                .Include(r => r.ChatMessages.OrderByDescending(m => m.CreatedAt).Take(1))
                    .ThenInclude(m => m.Sender)
                .FirstOrDefaultAsync(r => r.ChatRoomId == chatRoomId);

            if (room == null) return null;

            var dto = MapChatRoomToDTO(room, currentUserId);
            return dto;
        }

        public async Task<IEnumerable<ChatRoomDTO>> GetChatRoomsByUserAsync(int userId)
        {
            // Get all direct chat rooms where user is a participant
            var rooms = await _context.ChatRooms
                .Include(r => r.CreatedBy)
                .Include(r => r.ChatMessages.OrderByDescending(m => m.CreatedAt).Take(1))
                    .ThenInclude(m => m.Sender)
                .Where(r => r.IsActive == true && r.RoomType == "Direct" && 
                    (r.CreatedById == userId || r.RoomName!.Contains($"_{userId}_") || r.RoomName!.EndsWith($"_{userId}")))
                .OrderByDescending(r => r.ChatMessages.Max(m => m.CreatedAt))
                .ToListAsync();

            var result = new List<ChatRoomDTO>();
            foreach (var room in rooms)
            {
                var dto = MapChatRoomToDTO(room, userId);
                if (dto != null)
                {
                    result.Add(dto);
                }
            }
            return result;
        }

        public async Task<ChatRoomDTO?> FindDirectChatRoomAsync(int userId1, int userId2)
        {
            var roomName = GetDirectChatRoomName(userId1, userId2);
            var room = await _context.ChatRooms
                .Include(r => r.CreatedBy)
                .Include(r => r.ChatMessages.OrderByDescending(m => m.CreatedAt).Take(1))
                    .ThenInclude(m => m.Sender)
                .FirstOrDefaultAsync(r => r.RoomName == roomName && r.RoomType == "Direct" && r.IsActive == true);

            if (room == null) return null;
            return MapChatRoomToDTO(room, userId1);
        }

        public async Task<ChatRoomDTO?> GetOrCreateDirectChatRoomAsync(int userId1, int userId2)
        {
            var existingRoom = await FindDirectChatRoomAsync(userId1, userId2);
            if (existingRoom != null) return existingRoom;

            // Create new direct chat room
            var createDto = new CreateChatRoomDTO
            {
                RoomName = GetDirectChatRoomName(userId1, userId2),
                RoomType = "Direct",
                CreatedById = userId1,
                OtherUserId = userId2
            };

            return await CreateChatRoomAsync(createDto);
        }

        public async Task<ChatRoomDTO> CreateChatRoomAsync(CreateChatRoomDTO createDto)
        {
            var room = new ChatRoom
            {
                RoomName = createDto.RoomName,
                RoomType = createDto.RoomType,
                TeamId = createDto.TeamId,
                CreatedById = createDto.CreatedById,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.ChatRoomRepo.AddAsync(room);
            await _unitOfWork.SaveChangeAsync();

            // Reload with details
            return await GetChatRoomByIdAsync(room.ChatRoomId, createDto.CreatedById) 
                ?? new ChatRoomDTO { ChatRoomId = room.ChatRoomId };
        }

        public async Task<IEnumerable<ChatMessageDTO>> GetMessagesByChatRoomAsync(int chatRoomId, int currentUserId)
        {
            var messages = await _context.ChatMessages
                .Include(m => m.Sender)
                .Where(m => m.ChatRoomId == chatRoomId && m.IsDeleted != true)
                .OrderBy(m => m.CreatedAt)
                .ToListAsync();

            return messages.Select(m => MapChatMessageToDTO(m, currentUserId));
        }

        public async Task<IEnumerable<ChatMessageDTO>> GetRecentMessagesAsync(int chatRoomId, int count, int currentUserId)
        {
            var messages = await _context.ChatMessages
                .Include(m => m.Sender)
                .Where(m => m.ChatRoomId == chatRoomId && m.IsDeleted != true)
                .OrderByDescending(m => m.CreatedAt)
                .Take(count)
                .ToListAsync();

            return messages.OrderBy(m => m.CreatedAt).Select(m => MapChatMessageToDTO(m, currentUserId));
        }

        public async Task<ChatMessageDTO> SendMessageAsync(SendMessageDTO sendDto)
        {
            var message = new ChatMessage
            {
                ChatRoomId = sendDto.ChatRoomId,
                SenderId = sendDto.SenderId,
                MessageText = sendDto.MessageText,
                MessageType = sendDto.MessageType ?? "Text",
                AttachmentUrl = sendDto.AttachmentUrl,
                ReplyToMessageId = sendDto.ReplyToMessageId,
                IsEdited = false,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _unitOfWork.ChatMessageRepo.AddAsync(message);
            await _unitOfWork.SaveChangeAsync();

            // Reload with sender info
            var savedMessage = await _context.ChatMessages
                .Include(m => m.Sender)
                .FirstOrDefaultAsync(m => m.ChatMessageId == message.ChatMessageId);

            return MapChatMessageToDTO(savedMessage!, sendDto.SenderId);
        }

        public async Task<bool> DeleteMessageAsync(int messageId, int userId)
        {
            var message = await _unitOfWork.ChatMessageRepo.GetByIdAsync(messageId);
            if (message == null || message.SenderId != userId)
                return false;

            message.IsDeleted = true;
            message.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.ChatMessageRepo.Update(message);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<ChatMessageDTO?> EditMessageAsync(int messageId, string newText, int userId)
        {
            var message = await _context.ChatMessages
                .Include(m => m.Sender)
                .FirstOrDefaultAsync(m => m.ChatMessageId == messageId);

            if (message == null || message.SenderId != userId)
                return null;

            message.MessageText = newText;
            message.IsEdited = true;
            message.UpdatedAt = DateTime.UtcNow;
            await _unitOfWork.SaveChangeAsync();

            return MapChatMessageToDTO(message, userId);
        }

        public async Task<IEnumerable<TeacherForChatDTO>> GetTeachersForStudentChatAsync(int studentId)
        {
            // Get all cores the student is enrolled in (through team membership)
            var cores = await _context.Cores
                .Include(c => c.Instructor)
                .Include(c => c.Teams)
                    .ThenInclude(t => t.TeamMembers)
                .Where(c => c.IsActive == true && 
                    c.Teams.Any(t => t.LeaderId == studentId || t.TeamMembers.Any(tm => tm.UserId == studentId)))
                .ToListAsync();

            var teacherList = cores.Select(c => new TeacherForChatDTO
            {
                UserId = c.InstructorId,
                FullName = c.Instructor?.FullName ?? "Unknown",
                DisplayName = c.Instructor?.DisplayName,
                AvatarUrl = c.Instructor?.AvatarUrl,
                CoreId = c.CoreId,
                CoreName = c.CoreName,
                CoreCode = c.CoreCode
            }).DistinctBy(t => new { t.UserId, t.CoreId }).ToList();

            return teacherList;
        }

        public async Task<IEnumerable<ChatUserDTO>> GetStudentsForTeacherChatAsync(int teacherId)
        {
            // Get all students in cores where the teacher is instructor
            var cores = await _context.Cores
                .Include(c => c.Teams)
                    .ThenInclude(t => t.TeamMembers)
                        .ThenInclude(tm => tm.User)
                            .ThenInclude(u => u.Role)
                .Include(c => c.Teams)
                    .ThenInclude(t => t.Leader)
                        .ThenInclude(l => l!.Role)
                .Where(c => c.InstructorId == teacherId && c.IsActive == true)
                .ToListAsync();

            var students = new List<ChatUserDTO>();

            foreach (var core in cores)
            {
                foreach (var team in core.Teams)
                {
                    // Add team leader (only if they are a Student)
                    if (team.Leader != null && team.Leader.Role?.RoleName == "Student")
                    {
                        students.Add(new ChatUserDTO
                        {
                            UserId = team.Leader.UserId,
                            FullName = team.Leader.FullName,
                            DisplayName = team.Leader.DisplayName,
                            AvatarUrl = team.Leader.AvatarUrl,
                            RoleName = team.Leader.Role?.RoleName,
                            CoreId = core.CoreId,
                            CoreName = core.CoreName
                        });
                    }

                    // Add team members (only if they are Students)
                    foreach (var member in team.TeamMembers.Where(m => m.User.Role?.RoleName == "Student"))
                    {
                        students.Add(new ChatUserDTO
                        {
                            UserId = member.User.UserId,
                            FullName = member.User.FullName,
                            DisplayName = member.User.DisplayName,
                            AvatarUrl = member.User.AvatarUrl,
                            RoleName = member.User.Role?.RoleName,
                            CoreId = core.CoreId,
                            CoreName = core.CoreName
                        });
                    }
                }
            }

            return students.DistinctBy(s => s.UserId).ToList();
        }

        private ChatRoomDTO? MapChatRoomToDTO(ChatRoom room, int currentUserId)
        {
            // For direct chat, extract the other user's ID from room name
            int? otherUserId = null;
            string? otherUserName = null;
            string? otherUserAvatar = null;

            if (room.RoomType == "Direct" && room.RoomName != null)
            {
                var parts = room.RoomName.Replace("Direct_", "").Split('_');
                if (parts.Length == 2)
                {
                    var id1 = int.Parse(parts[0]);
                    var id2 = int.Parse(parts[1]);
                    otherUserId = id1 == currentUserId ? id2 : id1;
                }
            }

            // Get other user info if direct chat
            if (otherUserId.HasValue)
            {
                var otherUser = _context.Users.FirstOrDefault(u => u.UserId == otherUserId.Value);
                if (otherUser != null)
                {
                    otherUserName = otherUser.DisplayName ?? otherUser.FullName;
                    otherUserAvatar = otherUser.AvatarUrl;
                }
            }

            var lastMessage = room.ChatMessages?.FirstOrDefault();

            return new ChatRoomDTO
            {
                ChatRoomId = room.ChatRoomId,
                RoomName = room.RoomName,
                RoomType = room.RoomType,
                TeamId = room.TeamId,
                CreatedById = room.CreatedById,
                IsActive = room.IsActive,
                CreatedAt = room.CreatedAt,
                CreatedByName = room.CreatedBy?.FullName,
                OtherUserId = otherUserId,
                OtherUserName = otherUserName,
                OtherUserAvatar = otherUserAvatar,
                LastMessage = lastMessage != null ? MapChatMessageToDTO(lastMessage, currentUserId) : null
            };
        }

        private ChatMessageDTO MapChatMessageToDTO(ChatMessage message, int currentUserId)
        {
            return new ChatMessageDTO
            {
                ChatMessageId = message.ChatMessageId,
                ChatRoomId = message.ChatRoomId,
                SenderId = message.SenderId,
                SenderName = message.Sender?.DisplayName ?? message.Sender?.FullName,
                SenderAvatar = message.Sender?.AvatarUrl,
                MessageText = message.MessageText,
                MessageType = message.MessageType,
                AttachmentUrl = message.AttachmentUrl,
                IsEdited = message.IsEdited,
                IsDeleted = message.IsDeleted,
                ReplyToMessageId = message.ReplyToMessageId,
                CreatedAt = message.CreatedAt,
                UpdatedAt = message.UpdatedAt,
                IsOwnMessage = message.SenderId == currentUserId
            };
        }
    }
}
