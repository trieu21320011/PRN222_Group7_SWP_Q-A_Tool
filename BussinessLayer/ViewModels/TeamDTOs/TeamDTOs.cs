using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.ViewModels.TeamDTOs
{
    public class TeamDTO
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public string TeamCode { get; set; } = null!;
        public string? Description { get; set; }
        public string? ProjectName { get; set; }
        public int? LeaderId { get; set; }
        public string? LeaderName { get; set; }
        public int? MentorId { get; set; }
        public string? MentorName { get; set; }
        public string? Semester { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class CreateTeamDTO
    {
        public string TeamName { get; set; } = null!;
        public string TeamCode { get; set; } = null!;
        public string? Description { get; set; }
        public string? ProjectName { get; set; }
        public int? LeaderId { get; set; }
        public int? MentorId { get; set; }
        public string? Semester { get; set; }
        public bool? IsActive { get; set; }
    }

    public class UpdateTeamDTO
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public string TeamCode { get; set; } = null!;
        public string? Description { get; set; }
        public string? ProjectName { get; set; }
        public int? LeaderId { get; set; }
        public int? MentorId { get; set; }
        public string? Semester { get; set; }
        public bool? IsActive { get; set; }
    }

    public class GetTeamDTO
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public string TeamCode { get; set; } = null!;
        public string? Description { get; set; }
        public string? ProjectName { get; set; }
        public string? LeaderName { get; set; }
        public string? MentorName { get; set; }
        public string? Semester { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
