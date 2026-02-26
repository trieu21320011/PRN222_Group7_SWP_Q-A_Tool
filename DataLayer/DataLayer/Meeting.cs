using System;
using System.Collections.Generic;

namespace DataLayer.DataLayer;

public partial class Meeting
{
    public int MeetingId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string MeetingType { get; set; } = null!;

    public int OrganizerId { get; set; }

    public int? TeamId { get; set; }

    public string? MeetingUrl { get; set; }

    public string? Location { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string? TimeZone { get; set; }

    public int? MaxParticipants { get; set; }

    public int? CurrentParticipants { get; set; }

    public string? Status { get; set; }

    public bool? IsRecurring { get; set; }

    public string? RecurrencePattern { get; set; }

    public string? Color { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<MeetingParticipant> MeetingParticipants { get; set; } = new List<MeetingParticipant>();

    public virtual User Organizer { get; set; } = null!;

    public virtual Team? Team { get; set; }
}
