using BussinessLayer.IServices;
using BussinessLayer.ViewModels.SemesterDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PresentationLayer.Areas.Admin.Pages.Semesters
{
    public class DetailsModel : PageModel
    {
        private readonly ISemesterService _semesterService;

        public DetailsModel(ISemesterService semesterService)
        {
            _semesterService = semesterService;
        }

        public GetSemesterDTO? Semester { get; private set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Semester = await _semesterService.GetSemesterByIdAsync(id);
            if (Semester == null) return NotFound();
            return Page();
        }
    }
}
