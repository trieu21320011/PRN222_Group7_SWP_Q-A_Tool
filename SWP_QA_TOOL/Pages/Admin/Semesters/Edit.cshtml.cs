using BussinessLayer.IServices;
using BussinessLayer.ViewModels.SemesterDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PresentationLayer.Areas.Admin.Pages.Semesters
{
    public class EditModel : PageModel
    {
        private readonly ISemesterService _semesterService;

        public EditModel(ISemesterService semesterService)
        {
            _semesterService = semesterService;
        }

        [BindProperty]
        public UpdateSemesterDTO Input { get; set; } = new UpdateSemesterDTO();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var semester = await _semesterService.GetSemesterByIdAsync(id);
            if (semester == null) return NotFound();

            Input = new UpdateSemesterDTO
            {
                SemesterId = semester.SemesterId,
                SemesterCode = semester.SemesterCode,
                SemesterName = semester.SemesterName,
                StartDate = semester.StartDate,
                EndDate = semester.EndDate,
                IsActive = semester.IsActive,
                IsCurrent = semester.IsCurrent
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            if (Input.EndDate < Input.StartDate)
            {
                ModelState.AddModelError(nameof(Input.EndDate), "EndDate phải >= StartDate.");
                return Page();
            }

            var updated = await _semesterService.UpdateSemesterAsync(Input);
            if (updated == null)
            {
                ModelState.AddModelError(string.Empty, "Cập nhật thất bại.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
