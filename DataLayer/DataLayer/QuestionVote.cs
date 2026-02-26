using System;
using System.Collections.Generic;

namespace DataLayer.DataLayer;

public partial class QuestionVote
{
    public int QuestionVoteId { get; set; }

    public int QuestionId { get; set; }

    public int UserId { get; set; }

    public byte VoteType { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Question Question { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
