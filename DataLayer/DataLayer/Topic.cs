using System;
using System.Collections.Generic;

namespace DataLayer.DataLayer;

public partial class Topic
{
    public int TopicId { get; set; }

    public string TopicCode { get; set; } = null!;

    public string TopicName { get; set; } = null!;

    public string? Description { get; set; }

    public int SemesterId { get; set; }

    public string? Category { get; set; }

    public string? Difficulty { get; set; }

    public int? MaxTeams { get; set; }

    public int? CurrentTeams { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual Semester Semester { get; set; } = null!;

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
}
