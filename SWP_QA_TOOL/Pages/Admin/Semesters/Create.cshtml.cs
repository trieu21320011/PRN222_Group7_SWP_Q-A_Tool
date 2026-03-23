
using BussinessLayer.IServices;
using BussinessLayer.ViewModels.SemesterDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PresentationLayer.Areas.Admin.Pages.Semesters
{
    public class CreateModel : PageModel
    {
        private readonly ISemesterService _semesterService;

        public CreateModel(ISemesterService semesterService)
        {
            _semesterService = semesterService;
        }

        [BindProperty]
        public CreateSemesterDTO Input { get; set; } = new CreateSemesterDTO
        {
            IsActive = true,
            IsCurrent = false
        };

        public async Task OnGetAsync()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var startDate = today;

            var semesters = await _semesterService.GetAllSemestersAsync();
            var latestSemesterEndDate = semesters.OrderByDescending(s => s.EndDate).FirstOrDefault()?.EndDate;
            if (latestSemesterEndDate.HasValue && latestSemesterEndDate.Value >= today)
            {
                startDate = latestSemesterEndDate.Value.AddDays(1);
            }

            Input.StartDate = startDate;
            Input.EndDate = startDate.AddMonths(4);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            if (!await ValidateDatesAsync()) return Page();

            var created = await _semesterService.CreateSemesterAsync(Input);
            if (created == null || created.SemesterId <= 0)
            {
                ModelState.AddModelError(string.Empty, "Tạo Semester thất bại.");
                return Page();
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> ValidateDatesAsync()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            if (Input.StartDate < today)
            {
                ModelState.AddModelError(nameof(Input.StartDate), "Ngày bắt đầu của học kỳ mới phải từ hôm nay trở đi.");
            }

            if (Input.EndDate <= Input.StartDate)
            {
                ModelState.AddModelError(nameof(Input.EndDate), "Ngày kết thúc phải lớn hơn ngày bắt đầu.");
            }

            var semesters = await _semesterService.GetAllSemestersAsync();
            var isOverlapped = semesters.Any(s => Input.StartDate <= s.EndDate && Input.EndDate >= s.StartDate);
            if (isOverlapped)
            {
                ModelState.AddModelError(string.Empty, "Khoảng thời gian học kỳ bị trùng với học kỳ đã tồn tại.");
            }

            return ModelState.IsValid;
        }
    }
}