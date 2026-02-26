using System;
using System.Collections.Generic;

namespace DataLayer.DataLayer;

public partial class AnswerVote
{
    public int AnswerVoteId { get; set; }

    public int AnswerId { get; set; }

    public int UserId { get; set; }

    public byte VoteType { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Answer Answer { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
