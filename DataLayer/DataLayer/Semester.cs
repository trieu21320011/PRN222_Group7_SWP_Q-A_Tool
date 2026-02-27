using System;
using System.Collections.Generic;

namespace DataLayer.DataLayer;

public partial class Semester
{
    public int SemesterId { get; set; }

    public string SemesterCode { get; set; } = null!;

    public string SemesterName { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsCurrent { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Core> Cores { get; set; } = new List<Core>();

    public virtual ICollection<SystemInstructor> SystemInstructors { get; set; } = new List<SystemInstructor>();

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();

    public virtual ICollection<Topic> Topics { get; set; } = new List<Topic>();
}
