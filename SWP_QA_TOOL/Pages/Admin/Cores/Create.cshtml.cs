using BussinessLayer.IServices;
using BussinessLayer.ViewModels.CoreDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SWP_QA_TOOL.Pages.Admin.Cores
{
    public class CreateModel : PageModel
    {
        private readonly ICoreService _coreService;
        private readonly ISemesterService _semesterService;
        private readonly IUserService _userService;

        public CreateModel(ICoreService coreService, ISemesterService semesterService, IUserService userService)
        {
            _coreService = coreService;
            _semesterService = semesterService;
            _userService = userService;
        }

        [BindProperty]
        public CreateCoreDTO Input { get; set; } = new CreateCoreDTO
        {
            IsActive = true
        };

        public SelectList SemesterList { get; set; } = null!;
        public SelectList InstructorList { get; set; } = null!;

        public async Task OnGetAsync()
        {
            await LoadDropdownsAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return Page();
            }

            var created = await _coreService.CreateCoreAsync(Input);
            if (created == null || created.CoreId <= 0)
            {
                ModelState.AddModelError(string.Empty, "Tạo Core thất bại.");
                await LoadDropdownsAsync();
                return Page();
            }

            TempData["Success"] = "Tạo Core thành công.";
            return RedirectToPage("./Index");
        }

        private async Task LoadDropdownsAsync()
        {
            var semesters = await _semesterService.GetAllSemestersAsync();
            SemesterList = new SelectList(semesters, "SemesterId", "SemesterName");

            // Assuming RoleId = 2 is for Instructor/Lecturer
            var instructors = await _userService.GetUsersByRoleAsync(2);
            InstructorList = new SelectList(instructors, "UserId", "FullName");
        }
    }
}
