using BussinessLayer.IServices;
using BussinessLayer.ViewModels.SemesterDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PresentationLayer.Areas.Admin.Pages.Semesters
{
    public class DeleteModel : PageModel
    {
        private readonly ISemesterService _semesterService;

        public DeleteModel(ISemesterService semesterService)
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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var ok = await _semesterService.DeleteSemesterAsync(id);
            if (!ok)
            {
                TempData["Error"] = "Xóa thất bại (có thể đang bị ràng buộc dữ liệu).";
                return RedirectToPage("./Index");
            }

            return RedirectToPage("./Index");
        }
    }
}

