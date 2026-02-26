using System;
using System.Collections.Generic;

namespace DataLayer.DataLayer;

public partial class Tag
{
    public int TagId { get; set; }

    public string TagName { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? Description { get; set; }

    public string? Category { get; set; }

    public string? IconUrl { get; set; }

    public int? QuestionCount { get; set; }

    public int? FollowerCount { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<QuestionTag> QuestionTags { get; set; } = new List<QuestionTag>();

    public virtual ICollection<TagFollower> TagFollowers { get; set; } = new List<TagFollower>();
}
