using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.ViewModels.TagDTOs
{
    public class TagDTO
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
    }

    public class CreateTagDTO
    {
        public string TagName { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? IconUrl { get; set; }
        public bool? IsActive { get; set; }
    }

    public class UpdateTagDTO
    {
        public int TagId { get; set; }
        public string TagName { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? IconUrl { get; set; }
        public bool? IsActive { get; set; }
    }

    public class GetTagDTO
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
    }
}
