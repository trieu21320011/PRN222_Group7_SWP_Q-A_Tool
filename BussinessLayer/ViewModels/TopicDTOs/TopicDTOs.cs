using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.ViewModels.TopicDTOs
{
    public class TopicDTO
    {
        public int TopicId { get; set; }
        public string TopicCode { get; set; } = null!;
        public string TopicName { get; set; } = null!;
        public string? Description { get; set; }
        public int SemesterId { get; set; }
        public string? SemesterCode { get; set; }
        public string? Category { get; set; }
        public string? Difficulty { get; set; }
        public int? MaxTeams { get; set; }
        public int? CurrentTeams { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class CreateTopicDTO
    {
        public string TopicCode { get; set; } = null!;
        public string TopicName { get; set; } = null!;
        public string? Description { get; set; }
        public int SemesterId { get; set; }
        public string? Category { get; set; }
        public string? Difficulty { get; set; }
        public int? MaxTeams { get; set; }
        public bool? IsActive { get; set; }
    }

    public class UpdateTopicDTO
    {
        public int TopicId { get; set; }
        public string TopicCode { get; set; } = null!;
        public string TopicName { get; set; } = null!;
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? Difficulty { get; set; }
        public int? MaxTeams { get; set; }
        public bool? IsActive { get; set; }
    }

    public class GetTopicDTO
    {
        public int TopicId { get; set; }
        public string TopicCode { get; set; } = null!;
        public string TopicName { get; set; } = null!;
        public string? Description { get; set; }
        public string? SemesterCode { get; set; }
        public string? Category { get; set; }
        public string? Difficulty { get; set; }
        public int? MaxTeams { get; set; }
        public int? CurrentTeams { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
