using BussinessLayer.IServices;
using BussinessLayer.ViewModels.UserDTOs;
using DataLayer.DataLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SWP_QA_TOOL.Pages.Admin.Users
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly IUserService _userService;

        public CreateModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public CreateUserDTO Input { get; set; } = new CreateUserDTO();

        [BindProperty]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }

        public List<SelectListItem> RoleOptions { get; set; } = new();

        public async Task OnGetAsync()
        {
            await LoadRolesAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await LoadRolesAsync();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (string.IsNullOrWhiteSpace(Input.PasswordHash) || Input.PasswordHash.Length < 6)
            {
                ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.";
                return Page();
            }

            if (Input.PasswordHash != ConfirmPassword)
            {
                ErrorMessage = "Mật khẩu và xác nhận mật khẩu không khớp.";
                return Page();
            }

            var existingUser = await _userService.GetUserByEmailAsync(Input.Email);
            if (existingUser != null)
            {
                ErrorMessage = "Email đã tồn tại trong hệ thống.";
                return Page();
            }

            if (Input.RoleId == 0)
            {
                ErrorMessage = "Vui lòng chọn vai trò.";
                return Page();
            }

            try
            {
                var createdUser = await _userService.CreateUserAsync(Input);

                if (createdUser != null && createdUser.UserId > 0)
                {
                    TempData["SuccessMessage"] = $"Đã tạo thành công tài khoản cho {Input.FullName}.";
                    return RedirectToPage("./Index");
                }

                ErrorMessage = "Có lỗi xảy ra khi tạo người dùng. Vui lòng thử lại.";
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Lỗi: {ex.Message}";
            }

            return Page();
        }

        private async Task LoadRolesAsync()
        {
            var roles = await _userService.GetAllRolesAsync();
            RoleOptions = roles.Select(r => new SelectListItem
            {
                Value = r.RoleId.ToString(),
                Text = r.RoleName
            }).ToList();
        }
    }
}

