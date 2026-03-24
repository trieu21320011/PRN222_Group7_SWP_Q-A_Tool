using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.ViewModels.ChatDTOs
{
    public class ChatRoomDTO
    {
        public int ChatRoomId { get; set; }
        public string? RoomName { get; set; }
        public string RoomType { get; set; } = null!;
        public int? TeamId { get; set; }
        public int CreatedById { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedByName { get; set; }
        public int? OtherUserId { get; set; }
        public string? OtherUserName { get; set; }
        public string? OtherUserAvatar { get; set; }
        public ChatMessageDTO? LastMessage { get; set; }
        public int UnreadCount { get; set; }
    }

    public class CreateChatRoomDTO
    {
        public string? RoomName { get; set; }
        public string RoomType { get; set; } = "Direct";
        public int? TeamId { get; set; }
        public int CreatedById { get; set; }
        public int? OtherUserId { get; set; }
    }

    public class ChatMessageDTO
    {
        public int ChatMessageId { get; set; }
        public int ChatRoomId { get; set; }
        public int SenderId { get; set; }
        public string? SenderName { get; set; }
        public string? SenderAvatar { get; set; }
        public string MessageText { get; set; } = null!;
        public string? MessageType { get; set; }
        public string? AttachmentUrl { get; set; }
        public bool? IsEdited { get; set; }
        public bool? IsDeleted { get; set; }
        public int? ReplyToMessageId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsOwnMessage { get; set; }
    }

    public class SendMessageDTO
    {
        public int ChatRoomId { get; set; }
        public int SenderId { get; set; }
        public string MessageText { get; set; } = null!;
        public string? MessageType { get; set; } = "Text";
        public string? AttachmentUrl { get; set; }
        public int? ReplyToMessageId { get; set; }
    }

    public class ChatUserDTO
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string? DisplayName { get; set; }
        public string? AvatarUrl { get; set; }
        public string? RoleName { get; set; }
        public int? CoreId { get; set; }
        public string? CoreName { get; set; }
    }

    public class TeacherForChatDTO
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string? DisplayName { get; set; }
        public string? AvatarUrl { get; set; }
        public int CoreId { get; set; }
        public string CoreName { get; set; } = null!;
        public string CoreCode { get; set; } = null!;
    }
}
