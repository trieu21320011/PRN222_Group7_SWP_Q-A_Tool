using System;
using System.Collections.Generic;

namespace DataLayer.DataLayer;

public partial class Core
{
    public int CoreId { get; set; }

    public string CoreCode { get; set; } = null!;

    public string CoreName { get; set; } = null!;

    public int SemesterId { get; set; }

    public int InstructorId { get; set; }

    public int? MaxTeams { get; set; }

    public int? CurrentTeams { get; set; }

    public string? Schedule { get; set; }

    public string? Room { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User Instructor { get; set; } = null!;

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual Semester Semester { get; set; } = null!;

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
}
