using BussinessLayer.IServices;
using BussinessLayer.ViewModels.CoreDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SWP_QA_TOOL.Pages.Admin.Cores
{
    public class EditModel : PageModel
    {
        private readonly ICoreService _coreService;
        private readonly IUserService _userService;

        public EditModel(ICoreService coreService, IUserService userService)
        {
            _coreService = coreService;
            _userService = userService;
        }

        [BindProperty]
        public UpdateCoreDTO Input { get; set; } = new UpdateCoreDTO();

        public SelectList InstructorList { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var core = await _coreService.GetCoreByIdAsync(id);
            if (core == null) return NotFound();

            Input = new UpdateCoreDTO
            {
                CoreId = core.CoreId,
                CoreCode = core.CoreCode,
                CoreName = core.CoreName,
                InstructorId = 0, // Will need to be set from the actual data
                MaxTeams = core.MaxTeams,
                Schedule = core.Schedule,
                Room = core.Room,
                IsActive = core.IsActive
            };

            await LoadInstructorsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadInstructorsAsync();
                return Page();
            }

            var updated = await _coreService.UpdateCoreAsync(Input);
            if (updated == null)
            {
                ModelState.AddModelError(string.Empty, "Cập nhật thất bại.");
                await LoadInstructorsAsync();
                return Page();
            }

            TempData["Success"] = "Cập nhật Core thành công.";
            return RedirectToPage("./Index");
        }

        private async Task LoadInstructorsAsync()
        {
            // Assuming RoleId = 2 is for Instructor/Lecturer
            var instructors = await _userService.GetUsersByRoleAsync(2);
            InstructorList = new SelectList(instructors, "UserId", "FullName");
        }
    }
}
