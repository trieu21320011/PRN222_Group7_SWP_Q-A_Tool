using System;
using System.Collections.Generic;

namespace DataLayer.DataLayer;

public partial class Question
{
    public int QuestionId { get; set; }

    public string Title { get; set; } = null!;

    public string Body { get; set; } = null!;

    public string? Excerpt { get; set; }

    public int AuthorId { get; set; }

    public int? TeamId { get; set; }

    public string? Status { get; set; }

    public string? Category { get; set; }

    public string? Difficulty { get; set; }

    public int? ViewCount { get; set; }

    public int? AnswerCount { get; set; }

    public int? CommentCount { get; set; }

    public int? UpvoteCount { get; set; }

    public int? DownvoteCount { get; set; }

    public int? AcceptedAnswerId { get; set; }

    public bool? IsPinned { get; set; }

    public bool? IsClosed { get; set; }

    public string? ClosedReason { get; set; }

    public int? ClosedById { get; set; }

    public DateTime? ClosedAt { get; set; }

    public DateTime? LastActivityAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual User Author { get; set; } = null!;

    public virtual User? ClosedBy { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<QuestionFollower> QuestionFollowers { get; set; } = new List<QuestionFollower>();

    public virtual ICollection<QuestionTag> QuestionTags { get; set; } = new List<QuestionTag>();

    public virtual ICollection<QuestionVote> QuestionVotes { get; set; } = new List<QuestionVote>();

    public virtual Team? Team { get; set; }
}
