using System;
using System.Collections.Generic;

namespace DataLayer.DataLayer;

public partial class CommentVote
{
    public int CommentVoteId { get; set; }

    public int CommentId { get; set; }

    public int UserId { get; set; }

    public byte VoteType { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Comment Comment { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
