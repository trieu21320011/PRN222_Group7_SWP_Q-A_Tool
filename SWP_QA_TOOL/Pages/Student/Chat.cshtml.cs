using BussinessLayer.IServices;
using BussinessLayer.ViewModels.ChatDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace SWP_QA_TOOL.Pages.Student
{
    [Authorize(Roles = "Student")]
    public class ChatModel : PageModel
    {
        private readonly IChatService _chatService;

        public ChatModel(IChatService chatService)
        {
            _chatService = chatService;
        }

        public int CurrentUserId { get; set; }
        public IEnumerable<TeacherForChatDTO> AvailableTeachers { get; set; } = new List<TeacherForChatDTO>();
        public IEnumerable<ChatRoomDTO> ChatRooms { get; set; } = new List<ChatRoomDTO>();
        public int? SelectedChatRoomId { get; set; }
        public int? SelectedTeacherId { get; set; }
        public ChatRoomDTO? SelectedChatRoom { get; set; }
        public IEnumerable<ChatMessageDTO> Messages { get; set; } = new List<ChatMessageDTO>();

        public async Task<IActionResult> OnGetAsync(int? chatRoomId = null, int? teacherId = null)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return RedirectToPage("/Account/Login");
            }

            CurrentUserId = userId;

            // Get teachers available for chat (from student's enrolled cores)
            AvailableTeachers = await _chatService.GetTeachersForStudentChatAsync(userId);

            // Get existing chat rooms
            ChatRooms = await _chatService.GetChatRoomsByUserAsync(userId);

            // If teacherId is provided, start or get chat with that teacher
            if (teacherId.HasValue)
            {
                var chatRoom = await _chatService.GetOrCreateDirectChatRoomAsync(userId, teacherId.Value);
                if (chatRoom != null)
                {
                    SelectedChatRoomId = chatRoom.ChatRoomId;
                    SelectedChatRoom = chatRoom;
                    Messages = await _chatService.GetMessagesByChatRoomAsync(chatRoom.ChatRoomId, userId);
                    // Refresh chat rooms list
                    ChatRooms = await _chatService.GetChatRoomsByUserAsync(userId);
                }
                SelectedTeacherId = teacherId.Value;
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
