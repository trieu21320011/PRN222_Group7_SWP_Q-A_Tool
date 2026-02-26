using System;
using System.Collections.Generic;

namespace DataLayer.DataLayer;

public partial class Team
{
    public int TeamId { get; set; }

    public string TeamName { get; set; } = null!;

    public string TeamCode { get; set; } = null!;

    public string? Description { get; set; }

    public string? ProjectName { get; set; }

    public int? LeaderId { get; set; }

    public int? MentorId { get; set; }

    public string? Semester { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<ChatRoom> ChatRooms { get; set; } = new List<ChatRoom>();

    public virtual User? Leader { get; set; }

    public virtual ICollection<Meeting> Meetings { get; set; } = new List<Meeting>();

    public virtual User? Mentor { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();
}
