using BussinessLayer.ViewModels.ChatDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.IServices
{
    public interface IChatService
    {
        // Chat Room Methods
        Task<ChatRoomDTO?> GetChatRoomByIdAsync(int chatRoomId, int currentUserId);
        Task<IEnumerable<ChatRoomDTO>> GetChatRoomsByUserAsync(int userId);
        Task<ChatRoomDTO?> GetOrCreateDirectChatRoomAsync(int userId1, int userId2);
        Task<ChatRoomDTO> CreateChatRoomAsync(CreateChatRoomDTO createDto);
        
        // Chat Message Methods
        Task<IEnumerable<ChatMessageDTO>> GetMessagesByChatRoomAsync(int chatRoomId, int currentUserId);
        Task<IEnumerable<ChatMessageDTO>> GetRecentMessagesAsync(int chatRoomId, int count, int currentUserId);
        Task<ChatMessageDTO> SendMessageAsync(SendMessageDTO sendDto);
        Task<bool> DeleteMessageAsync(int messageId, int userId);
        Task<ChatMessageDTO?> EditMessageAsync(int messageId, string newText, int userId);
        
        // User Methods for Chat
        Task<IEnumerable<TeacherForChatDTO>> GetTeachersForStudentChatAsync(int studentId);
        Task<IEnumerable<ChatUserDTO>> GetStudentsForTeacherChatAsync(int teacherId);
        
        // Check existing chat room
        Task<ChatRoomDTO?> FindDirectChatRoomAsync(int userId1, int userId2);
    }
}
