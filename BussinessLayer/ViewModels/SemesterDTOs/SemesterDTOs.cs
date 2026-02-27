using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.ViewModels.SemesterDTOs
{
    public class SemesterDTO
    {
        public int SemesterId { get; set; }
        public string SemesterCode { get; set; } = null!;
        public string SemesterName { get; set; } = null!;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsCurrent { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class CreateSemesterDTO
    {
        public string SemesterCode { get; set; } = null!;
        public string SemesterName { get; set; } = null!;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsCurrent { get; set; }
    }

    public class UpdateSemesterDTO
    {
        public int SemesterId { get; set; }
        public string SemesterCode { get; set; } = null!;
        public string SemesterName { get; set; } = null!;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsCurrent { get; set; }
    }

    public class GetSemesterDTO
    {
        public int SemesterId { get; set; }
        public string SemesterCode { get; set; } = null!;
        public string SemesterName { get; set; } = null!;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsCurrent { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
