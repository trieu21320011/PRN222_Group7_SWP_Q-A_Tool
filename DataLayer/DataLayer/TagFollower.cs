using System;
using System.Collections.Generic;

namespace DataLayer.DataLayer;

public partial class TagFollower
{
    public int TagFollowerId { get; set; }

    public int TagId { get; set; }

    public int UserId { get; set; }

    public DateTime? FollowedAt { get; set; }

    public virtual Tag Tag { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
