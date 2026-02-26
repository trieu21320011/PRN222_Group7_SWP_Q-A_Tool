using System;
using System.Collections.Generic;

namespace DataLayer.DataLayer;

public partial class NotificationRecipient
{
    public int NotificationRecipientId { get; set; }

    public int NotificationId { get; set; }

    public int UserId { get; set; }

    public bool? IsRead { get; set; }

    public DateTime? ReadAt { get; set; }

    public bool? IsEmailSent { get; set; }

    public DateTime? EmailSentAt { get; set; }

    public virtual Notification Notification { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
