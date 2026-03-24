using BussinessLayer.IServices;
using BussinessLayer.ViewModels.ChatDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace SWP_QA_TOOL.Pages.Instructor
{
    [Authorize(Roles = "Instructor,Mentor")]
    public class ChatModel : PageModel
    {
        private readonly IChatService _chatService;

        public ChatModel(IChatService chatService)
        {
            _chatService = chatService;
        }

        public int CurrentUserId { get; set; }
        public IEnumerable<ChatUserDTO> AvailableStudents { get; set; } = new List<ChatUserDTO>();
        public IEnumerable<ChatRoomDTO> ChatRooms { get; set; } = new List<ChatRoomDTO>();
        public int? SelectedChatRoomId { get; set; }
        public int? SelectedStudentId { get; set; }
        public ChatRoomDTO? SelectedChatRoom { get; set; }
        public IEnumerable<ChatMessageDTO> Messages { get; set; } = new List<ChatMessageDTO>();

        // Group students by Core
        public Dictionary<string, List<ChatUserDTO>> StudentsByCore { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int? chatRoomId = null, int? studentId = null)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return RedirectToPage("/Account/Login");
            }

            CurrentUserId = userId;

            // Get students available for chat (from instructor's cores)
            AvailableStudents = await _chatService.GetStudentsForTeacherChatAsync(userId);

            // Group students by Core
            StudentsByCore = AvailableStudents
                .GroupBy(s => s.CoreName ?? "Không xác định")
                .ToDictionary(g => g.Key, g => g.ToList());

            // Get existing chat rooms
            ChatRooms = await _chatService.GetChatRoomsByUserAsync(userId);

            // If studentId is provided, start or get chat with that student
            if (studentId.HasValue)
            {
                var chatRoom = await _chatService.GetOrCreateDirectChatRoomAsync(userId, studentId.Value);
                if (chatRoom != null)
                {
                    SelectedChatRoomId = chatRoom.ChatRoomId;
                    SelectedChatRoom = chatRoom;
                    Messages = await _chatService.GetMessagesByChatRoomAsync(chatRoom.ChatRoomId, userId);
                    // Refresh chat rooms list
                    ChatRooms = await _chatService.GetChatRoomsByUserAsync(userId);
                }
                SelectedStudentId = studentId.Value;
            }
            // If chatRoomId is provided, load that chat
            else if (chatRoomId.HasValue)
            {
                SelectedChatRoomId = chatRoomId.Value;
                SelectedChatRoom = await _chatService.GetChatRoomByIdAsync(chatRoomId.Value, userId);
                Messages = await _chatService.GetMessagesByChatRoomAsync(chatRoomId.Value, userId);
            }

            return Page();
        }
    }
}
