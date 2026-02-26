using System;
using System.Collections.Generic;

namespace DataLayer.DataLayer;

public partial class QuestionFollower
{
    public int QuestionFollowerId { get; set; }

    public int QuestionId { get; set; }

    public int UserId { get; set; }

    public bool? NotifyOnAnswer { get; set; }

    public bool? NotifyOnComment { get; set; }

    public DateTime? FollowedAt { get; set; }

    public virtual Question Question { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
