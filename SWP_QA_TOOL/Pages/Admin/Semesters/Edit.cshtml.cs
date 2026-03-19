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

            if (!await ValidateDatesAsync()) return Page();

            var updated = await _semesterService.UpdateSemesterAsync(Input);
            if (updated == null)
            {
                ModelState.AddModelError(string.Empty, "Cập nhật thất bại.");
                return Page();
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> ValidateDatesAsync()
        {
            if (Input.EndDate <= Input.StartDate)
            {
                ModelState.AddModelError(nameof(Input.EndDate), "Ngày kết thúc phải lớn hơn ngày bắt đầu.");
            }

            var semesters = await _semesterService.GetAllSemestersAsync();
            var isOverlapped = semesters
                .Where(s => s.SemesterId != Input.SemesterId)
                .Any(s => Input.StartDate <= s.EndDate && Input.EndDate >= s.StartDate);

            if (isOverlapped)
            {
                ModelState.AddModelError(string.Empty, "Khoảng thời gian học kỳ bị trùng với học kỳ đã tồn tại.");
            }

            return ModelState.IsValid;
        }
    }
}
