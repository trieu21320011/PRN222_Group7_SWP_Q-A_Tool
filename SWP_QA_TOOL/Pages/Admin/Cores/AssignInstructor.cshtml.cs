using BussinessLayer.IServices;
using BussinessLayer.ViewModels.CoreDTOs;
using BussinessLayer.ViewModels.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SWP_QA_TOOL.Pages.Admin.Cores
{
    [Authorize(Roles = "Admin")]
    public class AssignInstructorModel : PageModel
    {
        private readonly ICoreService _coreService;
        private readonly IUserService _userService;
        private readonly ISemesterService _semesterService;

        public AssignInstructorModel(ICoreService coreService, IUserService userService, ISemesterService semesterService)
        {
            _coreService = coreService;
            _userService = userService;
            _semesterService = semesterService;
        }

        public IEnumerable<GetCoreDTO> Cores { get; set; } = new List<GetCoreDTO>();
        public IEnumerable<GetUserDTO> Instructors { get; set; } = new List<GetUserDTO>();
        public SelectList SemesterList { get; set; } = null!;

        [BindProperty(SupportsGet = true)]
        public int? SemesterId { get; set; }

        [BindProperty]
        public int CoreId { get; set; }

        [BindProperty]
        public int InstructorId { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadDataAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAssignAsync()
        {
            if (CoreId <= 0 || InstructorId <= 0)
            {
                TempData["Error"] = "Vui lòng chọn lớp học và giảng viên.";
                await LoadDataAsync();
                return Page();
            }

            var core = await _coreService.GetCoreByIdAsync(CoreId);
            if (core == null)
            {
                TempData["Error"] = "Không tìm thấy lớp học.";
                await LoadDataAsync();
                return Page();
            }

            var updateDto = new UpdateCoreDTO
            {
                CoreId = core.CoreId,
                CoreCode = core.CoreCode,
                CoreName = core.CoreName,
                InstructorId = InstructorId,
                MaxTeams = core.MaxTeams,
                Schedule = core.Schedule,
                Room = core.Room,
                IsActive = core.IsActive
            };

            var result = await _coreService.UpdateCoreAsync(updateDto);
            if (result == null)
            {
                TempData["Error"] = "Phân công giảng viên thất bại.";
                await LoadDataAsync();
                return Page();
            }

            TempData["Success"] = "Phân công giảng viên thành công!";
            return RedirectToPage(new { SemesterId });
        }

        private async Task LoadDataAsync()
        {
            // Load semesters
            var semesters = await _semesterService.GetAllSemestersAsync();
            SemesterList = new SelectList(semesters, "SemesterId", "SemesterCode");

            // Load cores
            if (SemesterId.HasValue && SemesterId.Value > 0)
            {
                Cores = await _coreService.GetCoresBySemesterAsync(SemesterId.Value);
            }
            else
            {
                Cores = await _coreService.GetAllCoresAsync();
            }

            // Load instructors (RoleId = 2 for Instructor)
            Instructors = await _userService.GetUsersByRoleAsync(2);
        }
    }
}
