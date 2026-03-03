
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

        public void OnGet()
        {
            // just render page
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            if (Input.EndDate < Input.StartDate)
            {
                ModelState.AddModelError(nameof(Input.EndDate), "EndDate phải >= StartDate.");
                return Page();
            }

            var created = await _semesterService.CreateSemesterAsync(Input);
            if (created == null || created.SemesterId <= 0)
            {
                ModelState.AddModelError(string.Empty, "Tạo Semester thất bại.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}