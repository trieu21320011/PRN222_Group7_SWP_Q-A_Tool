using BussinessLayer;
using BussinessLayer.IServices;
using BussinessLayer.ViewModels.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SWP_QA_TOOL.Pages.Admin.Users
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public EditModel(IUserService userService, IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public EditUserInputModel Input { get; set; } = new();

        public SelectList RoleList { get; set; } = null!;

        public class EditUserInputModel
        {
            public int UserId { get; set; }

            [Required(ErrorMessage = "Email là bắt buộc")]
            [EmailAddress(ErrorMessage = "Email không hợp lệ")]
            public string Email { get; set; } = null!;

            [Required(ErrorMessage = "Họ tên là bắt buộc")]
            public string FullName { get; set; } = null!;

            public string? DisplayName { get; set; }

            public string? StudentId { get; set; }

            [Required(ErrorMessage = "Vai trò là bắt buộc")]
            public int RoleId { get; set; }

            public bool IsActive { get; set; } = true;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            Input = new EditUserInputModel
            {
                UserId = user.UserId,
                Email = user.Email,
                FullName = user.FullName,
                DisplayName = user.DisplayName,
                StudentId = user.StudentId,
                RoleId = GetRoleIdFromName(user.RoleName),
                IsActive = user.IsActive ?? true
            };

            await LoadRolesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadRolesAsync();
                return Page();
            }

            // Check if email already exists (for other users)
            var existingUser = await _userService.GetUserByEmailAsync(Input.Email);
            if (existingUser != null && existingUser.UserId != Input.UserId)
            {
                ModelState.AddModelError("Input.Email", "Email đã tồn tại trong hệ thống");
                await LoadRolesAsync();
                return Page();
            }

            var updateDto = new UpdateUserDTO
            {
                UserId = Input.UserId,
                Email = Input.Email,
                FullName = Input.FullName,
                DisplayName = Input.DisplayName,
                StudentId = Input.StudentId,
                RoleId = Input.RoleId,
                IsActive = Input.IsActive
            };

            var result = await _userService.UpdateUserAsync(updateDto);
            if (result != null)
            {
                TempData["Success"] = "Cập nhật người dùng thành công!";
                return RedirectToPage("Index");
            }

            TempData["Error"] = "Cập nhật người dùng thất bại. Vui lòng thử lại.";
            await LoadRolesAsync();
            return Page();
        }

        private async Task LoadRolesAsync()
        {
            var roles = await _unitOfWork.RoleRepo.GetAllAsync();
            RoleList = new SelectList(roles, "RoleId", "RoleName", Input.RoleId);
        }

        private int GetRoleIdFromName(string? roleName)
        {
            return roleName switch
            {
                "Admin" => 1,
                "Instructor" => 2,
                "Student" => 3,
                _ => 3
            };
        }
    }
}
