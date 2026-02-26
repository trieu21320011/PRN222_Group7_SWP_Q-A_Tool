using System;
using System.Collections.Generic;

namespace DataLayer.DataLayer;

public partial class Notification
{
    public int NotificationId { get; set; }

    public string Title { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string NotificationType { get; set; } = null!;

    public string? ReferenceType { get; set; }

    public int? ReferenceId { get; set; }

    public int? CreatedById { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User? CreatedBy { get; set; }

    public virtual ICollection<NotificationRecipient> NotificationRecipients { get; set; } = new List<NotificationRecipient>();
}
