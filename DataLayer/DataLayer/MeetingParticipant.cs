using System;
using System.Collections.Generic;

namespace DataLayer.DataLayer;

public partial class MeetingParticipant
{
    public int MeetingParticipantId { get; set; }

    public int MeetingId { get; set; }

    public int UserId { get; set; }

    public string? Status { get; set; }

    public DateTime? RegisteredAt { get; set; }

    public DateTime? AttendedAt { get; set; }

    public virtual Meeting Meeting { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
