using BussinessLayer.IServices;
using BussinessLayer.ViewModels.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SWP_QA_TOOL.Pages.Admin.Users
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public IEnumerable<GetUserDTO> Users { get; set; } = new List<GetUserDTO>();

        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? RoleFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? StatusFilter { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var allUsers = await _userService.GetAllUsersAsync();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                allUsers = allUsers.Where(u => 
                    u.FullName.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                    u.Email.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(RoleFilter))
            {
                allUsers = allUsers.Where(u => u.RoleName == RoleFilter);
            }

            if (!string.IsNullOrEmpty(StatusFilter))
            {
                bool isActive = StatusFilter == "active";
                allUsers = allUsers.Where(u => u.IsActive == isActive);
            }

            Users = allUsers.ToList();
            return Page();
        }
    }
}
