using System;
using System.Collections.Generic;

namespace DataLayer.DataLayer;

public partial class Answer
{
    public int AnswerId { get; set; }

    public int QuestionId { get; set; }

    public int AuthorId { get; set; }

    public string Body { get; set; } = null!;

    public bool? IsAccepted { get; set; }

    public bool? IsMentorAnswer { get; set; }

    public bool? IsAigenerated { get; set; }

    public int? UpvoteCount { get; set; }

    public int? DownvoteCount { get; set; }

    public int? CommentCount { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<AnswerVote> AnswerVotes { get; set; } = new List<AnswerVote>();

    public virtual User Author { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Question Question { get; set; } = null!;
}
