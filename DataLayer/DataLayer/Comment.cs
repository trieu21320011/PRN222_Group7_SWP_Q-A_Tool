using System;
using System.Collections.Generic;

namespace DataLayer.DataLayer;

public partial class Comment
{
    public int CommentId { get; set; }

    public string Body { get; set; } = null!;

    public int AuthorId { get; set; }

    public int? QuestionId { get; set; }

    public int? AnswerId { get; set; }

    public int? ParentCommentId { get; set; }

    public bool? IsEdited { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Answer? Answer { get; set; }

    public virtual User Author { get; set; } = null!;

    public virtual ICollection<Comment> InverseParentComment { get; set; } = new List<Comment>();

    public virtual Comment? ParentComment { get; set; }

    public virtual Question? Question { get; set; }
}
