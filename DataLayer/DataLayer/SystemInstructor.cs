using System;
using System.Collections.Generic;

namespace DataLayer.DataLayer;

public partial class SystemInstructor
{
    public int SystemInstructorId { get; set; }

    public int UserId { get; set; }

    public int SemesterId { get; set; }

    public bool? IsHead { get; set; }

    public bool? CanManageTopics { get; set; }

    public bool? CanManageCores { get; set; }

    public bool? CanManageInstructors { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Semester Semester { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
