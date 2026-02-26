using System;
using System.Collections.Generic;

namespace DataLayer.DataLayer;

public partial class ChatRoom
{
    public int ChatRoomId { get; set; }

    public string? RoomName { get; set; }

    public string RoomType { get; set; } = null!;

    public int? TeamId { get; set; }

    public int CreatedById { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();

    public virtual User CreatedBy { get; set; } = null!;

    public virtual Team? Team { get; set; }
}
