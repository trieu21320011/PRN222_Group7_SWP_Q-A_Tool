using BussinessLayer.IServices;
using BussinessLayer.ViewModels.SemesterDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SWP_QA_TOOL.Pages.Instructor.Semesters
{
    [Authorize(Roles = "Instructor,Mentor")]
    public class IndexModel : PageModel
    {
        private readonly ISemesterService _semesterService;

        public IndexModel(ISemesterService semesterService)
        {
            _semesterService = semesterService;
        }

        public IEnumerable<GetSemesterDTO> Semesters { get; set; } = new List<GetSemesterDTO>();

        public async Task OnGetAsync()
        {
            var allSemesters = await _semesterService.GetAllSemestersAsync();
            Semesters = allSemesters?.OrderByDescending(s => s.StartDate).ToList() ?? new List<GetSemesterDTO>();
        }
    }
}
