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
        public string? RoleName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Status { get; set; }

        public async Task OnGetAsync()
        {
            var users = await _userService.GetAllUsersAsync();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                SearchTerm = SearchTerm.ToLower();
                users = users.Where(u => 
                    (u.FullName != null && u.FullName.ToLower().Contains(SearchTerm)) ||
                    (u.Email != null && u.Email.ToLower().Contains(SearchTerm)) ||
                    (u.StudentId != null && u.StudentId.ToLower().Contains(SearchTerm))
                );
            }

            if (!string.IsNullOrEmpty(RoleName) && RoleName != "ALL")
            {
                users = users.Where(u => u.RoleName == RoleName);
            }

            if (!string.IsNullOrEmpty(Status) && Status != "ALL")
            {
                bool isActive = Status == "Active";
                users = users.Where(u => u.IsActive == isActive);
            }

            Users = users.OrderByDescending(u => u.CreatedAt).ToList();
        }
    }
}
