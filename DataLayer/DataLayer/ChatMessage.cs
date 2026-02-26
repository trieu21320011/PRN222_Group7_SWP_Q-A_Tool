using System;
using System.Collections.Generic;

namespace DataLayer.DataLayer;

public partial class ChatMessage
{
    public int ChatMessageId { get; set; }

    public int ChatRoomId { get; set; }

    public int SenderId { get; set; }

    public string MessageText { get; set; } = null!;

    public string? MessageType { get; set; }

    public string? AttachmentUrl { get; set; }

    public bool? IsEdited { get; set; }

    public bool? IsDeleted { get; set; }

    public int? ReplyToMessageId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ChatRoom ChatRoom { get; set; } = null!;

    public virtual ICollection<ChatMessage> InverseReplyToMessage { get; set; } = new List<ChatMessage>();

    public virtual ChatMessage? ReplyToMessage { get; set; }

    public virtual User Sender { get; set; } = null!;
}
