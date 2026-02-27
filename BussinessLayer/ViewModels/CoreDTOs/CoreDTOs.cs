using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.ViewModels.CoreDTOs
{
    public class CoreDTO
    {
        public int CoreId { get; set; }
        public string CoreCode { get; set; } = null!;
        public string CoreName { get; set; } = null!;
        public int SemesterId { get; set; }
        public string? SemesterCode { get; set; }
        public int InstructorId { get; set; }
        public string? InstructorName { get; set; }
        public int? MaxTeams { get; set; }
        public int? CurrentTeams { get; set; }
        public string? Schedule { get; set; }
        public string? Room { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class CreateCoreDTO
    {
        public string CoreCode { get; set; } = null!;
        public string CoreName { get; set; } = null!;
        public int SemesterId { get; set; }
        public int InstructorId { get; set; }
        public int? MaxTeams { get; set; }
        public string? Schedule { get; set; }
        public string? Room { get; set; }
        public bool? IsActive { get; set; }
    }

    public class UpdateCoreDTO
    {
        public int CoreId { get; set; }
        public string CoreCode { get; set; } = null!;
        public string CoreName { get; set; } = null!;
        public int InstructorId { get; set; }
        public int? MaxTeams { get; set; }
        public string? Schedule { get; set; }
        public string? Room { get; set; }
        public bool? IsActive { get; set; }
    }

    public class GetCoreDTO
    {
        public int CoreId { get; set; }
        public string CoreCode { get; set; } = null!;
        public string CoreName { get; set; } = null!;
        public string? SemesterCode { get; set; }
        public string? InstructorName { get; set; }
        public int? MaxTeams { get; set; }
        public int? CurrentTeams { get; set; }
        public string? Schedule { get; set; }
        public string? Room { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
