using BussinessLayer;
using BussinessLayer.IServices;
using BussinessLayer.ViewModels.UserDTOs;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace SWP_QA_TOOL.Pages.Admin.Users
{
    [Authorize(Roles = "Admin")]
    public class ImportModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public ImportModel(IUserService userService, IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public IFormFile? ExcelFile { get; set; }

        [BindProperty]
        public string? ImportDataJson { get; set; }

        public List<ImportUserRow> ImportedUsers { get; set; } = new();
        public SelectList RoleList { get; set; } = null!;
        public Dictionary<int, string> RoleMap { get; set; } = new();
        public bool ShowPreview { get; set; } = false;
        public int ValidCount { get; set; }
        public int InvalidCount { get; set; }

        public class ImportUserRow
        {
            public int RowNumber { get; set; }
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public string FullName { get; set; } = string.Empty;
            public string? DisplayName { get; set; }
            public string? StudentId { get; set; }
            public string RoleName { get; set; } = string.Empty;
            public int? RoleId { get; set; }
            public bool IsActive { get; set; } = true;
            public List<string> Errors { get; set; } = new();
            public bool IsValid => Errors.Count == 0;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadRolesAsync();
            return Page();
        }

        public IActionResult OnGetDownloadTemplate()
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Users");

            // Header row
            var headers = new[] { "Email*", "Password*", "FullName*", "DisplayName", "StudentId", "RoleName*", "IsActive" };
            for (int i = 0; i < headers.Length; i++)
            {
                var cell = worksheet.Cell(1, i + 1);
                cell.Value = headers[i];
                cell.Style.Font.Bold = true;
                cell.Style.Fill.BackgroundColor = XLColor.LightBlue;
                cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            }

            // Instructions row
            worksheet.Cell(2, 1).Value = "example@fpt.edu.vn";
            worksheet.Cell(2, 2).Value = "password123";
            worksheet.Cell(2, 3).Value = "Nguyen Van A";
            worksheet.Cell(2, 4).Value = "Van A";
            worksheet.Cell(2, 5).Value = "SE123456";
            worksheet.Cell(2, 6).Value = "Student";
            worksheet.Cell(2, 7).Value = "true";

            // Set row 2 as example (italic, gray)
            var exampleRow = worksheet.Row(2);
            exampleRow.Style.Font.Italic = true;
            exampleRow.Style.Font.FontColor = XLColor.Gray;

            // Add instructions sheet
            var instructionsSheet = workbook.Worksheets.Add("Instructions");
            instructionsSheet.Cell(1, 1).Value = "Hướng dẫn import người dùng";
            instructionsSheet.Cell(1, 1).Style.Font.Bold = true;
            instructionsSheet.Cell(1, 1).Style.Font.FontSize = 14;

            instructionsSheet.Cell(3, 1).Value = "Các trường bắt buộc (có dấu *):";
            instructionsSheet.Cell(3, 1).Style.Font.Bold = true;
            instructionsSheet.Cell(4, 1).Value = "- Email: Email hợp lệ, không trùng với user đã có trong hệ thống";
            instructionsSheet.Cell(5, 1).Value = "- Password: Mật khẩu tối thiểu 6 ký tự";
            instructionsSheet.Cell(6, 1).Value = "- FullName: Họ và tên đầy đủ";
            instructionsSheet.Cell(7, 1).Value = "- RoleName: Tên vai trò (Admin, Teacher, Student, ...)";

            instructionsSheet.Cell(9, 1).Value = "Các trường không bắt buộc:";
            instructionsSheet.Cell(9, 1).Style.Font.Bold = true;
            instructionsSheet.Cell(10, 1).Value = "- DisplayName: Tên hiển thị";
            instructionsSheet.Cell(11, 1).Value = "- StudentId: Mã sinh viên";
            instructionsSheet.Cell(12, 1).Value = "- IsActive: true/false (mặc định: true)";

            instructionsSheet.Cell(14, 1).Value = "Lưu ý:";
            instructionsSheet.Cell(14, 1).Style.Font.Bold = true;
            instructionsSheet.Cell(15, 1).Value = "- Dòng 2 trong sheet Users là dòng mẫu, hãy xóa trước khi import";
            instructionsSheet.Cell(16, 1).Value = "- Bắt đầu nhập dữ liệu từ dòng 2 (sau dòng header)";

            // Auto-fit columns
            worksheet.Columns().AdjustToContents();
            instructionsSheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(), 
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", 
                "UserImportTemplate.xlsx");
        }

        public async Task<IActionResult> OnPostUploadAsync()
        {
            await LoadRolesAsync();

            if (ExcelFile == null || ExcelFile.Length == 0)
            {
                ModelState.AddModelError("ExcelFile", "Vui lòng chọn file Excel");
                return Page();
            }

            var extension = Path.GetExtension(ExcelFile.FileName).ToLowerInvariant();
            if (extension != ".xlsx" && extension != ".xls")
            {
                ModelState.AddModelError("ExcelFile", "Chỉ chấp nhận file Excel (.xlsx, .xls)");
                return Page();
            }

            try
            {
                using var stream = new MemoryStream();
                await ExcelFile.CopyToAsync(stream);
                stream.Position = 0;

                using var workbook = new XLWorkbook(stream);
                var worksheet = workbook.Worksheets.First();

                var lastRow = worksheet.LastRowUsed()?.RowNumber() ?? 1;
                var existingEmails = (await _userService.GetAllUsersAsync())
                    .Select(u => u.Email.ToLowerInvariant())
                    .ToHashSet();

                var emailsInFile = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                for (int row = 2; row <= lastRow; row++)
                {
                    var email = worksheet.Cell(row, 1).GetString()?.Trim() ?? "";
                    var password = worksheet.Cell(row, 2).GetString()?.Trim() ?? "";
                    var fullName = worksheet.Cell(row, 3).GetString()?.Trim() ?? "";
                    var displayName = worksheet.Cell(row, 4).GetString()?.Trim();
                    var studentId = worksheet.Cell(row, 5).GetString()?.Trim();
                    var roleName = worksheet.Cell(row, 6).GetString()?.Trim() ?? "";
                    var isActiveStr = worksheet.Cell(row, 7).GetString()?.Trim()?.ToLowerInvariant() ?? "true";

                    // Skip empty rows
                    if (string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(fullName))
                        continue;

                    var userRow = new ImportUserRow
                    {
                        RowNumber = row,
                        Email = email,
                        Password = password,
                        FullName = fullName,
                        DisplayName = string.IsNullOrWhiteSpace(displayName) ? null : displayName,
                        StudentId = string.IsNullOrWhiteSpace(studentId) ? null : studentId,
                        RoleName = roleName,
                        IsActive = isActiveStr == "true" || isActiveStr == "1" || isActiveStr == "yes"
                    };

                    // Validate
                    ValidateUserRow(userRow, existingEmails, emailsInFile);

                    // Map RoleName to RoleId
                    var matchedRole = RoleMap.FirstOrDefault(r => 
                        r.Value.Equals(roleName, StringComparison.OrdinalIgnoreCase));
                    if (matchedRole.Key > 0)
                    {
                        userRow.RoleId = matchedRole.Key;
                    }

                    ImportedUsers.Add(userRow);
                    
                    if (!string.IsNullOrWhiteSpace(email))
                        emailsInFile.Add(email);
                }

                ShowPreview = true;
                ValidCount = ImportedUsers.Count(u => u.IsValid);
                InvalidCount = ImportedUsers.Count(u => !u.IsValid);

                // Store data for submit
                ImportDataJson = JsonSerializer.Serialize(ImportedUsers);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("ExcelFile", $"Lỗi đọc file Excel: {ex.Message}");
                return Page();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            if (string.IsNullOrEmpty(ImportDataJson))
            {
                TempData["Error"] = "Không có dữ liệu để import";
                return RedirectToPage();
            }

            await LoadRolesAsync();

            var users = JsonSerializer.Deserialize<List<ImportUserRow>>(ImportDataJson);
            if (users == null || users.Count == 0)
            {
                TempData["Error"] = "Không có dữ liệu để import";
                return RedirectToPage();
            }

            // Re-validate before saving
            var existingEmails = (await _userService.GetAllUsersAsync())
                .Select(u => u.Email.ToLowerInvariant())
                .ToHashSet();

            var emailsInFile = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var user in users)
            {
                user.Errors.Clear();
                ValidateUserRow(user, existingEmails, emailsInFile);
                
                var matchedRole = RoleMap.FirstOrDefault(r => 
                    r.Value.Equals(user.RoleName, StringComparison.OrdinalIgnoreCase));
                if (matchedRole.Key > 0)
                {
                    user.RoleId = matchedRole.Key;
                }
                
                if (!string.IsNullOrWhiteSpace(user.Email))
                    emailsInFile.Add(user.Email);
            }

            var validUsers = users.Where(u => u.IsValid).ToList();

            if (validUsers.Count == 0)
            {
                ImportedUsers = users;
                ShowPreview = true;
                ValidCount = 0;
                InvalidCount = users.Count;
                ImportDataJson = JsonSerializer.Serialize(users);
                TempData["Error"] = "Không có người dùng hợp lệ để import";
                return Page();
            }

            int successCount = 0;
            int failCount = 0;

            foreach (var user in validUsers)
            {
                try
                {
                    var createDto = new CreateUserDTO
                    {
                        Email = user.Email,
                        PasswordHash = user.Password,
                        FullName = user.FullName,
                        DisplayName = user.DisplayName,
                        StudentId = user.StudentId,
                        RoleId = user.RoleId ?? 1,
                        IsActive = user.IsActive,
                        IsEmailVerified = false
                    };

                    var result = await _userService.CreateUserAsync(createDto);
                    if (result.UserId > 0)
                    {
                        successCount++;
                    }
                    else
                    {
                        failCount++;
                    }
                }
                catch
                {
                    failCount++;
                }
            }

            if (successCount > 0)
            {
                TempData["Success"] = $"Import thành công {successCount} người dùng" + 
                    (failCount > 0 ? $", thất bại {failCount} người dùng" : "");
            }
            else
            {
                TempData["Error"] = "Import thất bại. Vui lòng thử lại.";
            }

            return RedirectToPage("Index");
        }

        private void ValidateUserRow(ImportUserRow user, HashSet<string> existingEmails, HashSet<string> emailsInFile)
        {
            // Email validation
            if (string.IsNullOrWhiteSpace(user.Email))
            {
                user.Errors.Add("Email là bắt buộc");
            }
            else
            {
                if (!new EmailAddressAttribute().IsValid(user.Email))
                {
                    user.Errors.Add("Email không hợp lệ");
                }
                else if (existingEmails.Contains(user.Email.ToLowerInvariant()))
                {
                    user.Errors.Add("Email đã tồn tại trong hệ thống");
                }
                else if (emailsInFile.Contains(user.Email))
                {
                    user.Errors.Add("Email bị trùng trong file");
                }
            }

            // Password validation
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                user.Errors.Add("Mật khẩu là bắt buộc");
            }
            else if (user.Password.Length < 6)
            {
                user.Errors.Add("Mật khẩu tối thiểu 6 ký tự");
            }

            // FullName validation
            if (string.IsNullOrWhiteSpace(user.FullName))
            {
                user.Errors.Add("Họ tên là bắt buộc");
            }

            // RoleName validation
            if (string.IsNullOrWhiteSpace(user.RoleName))
            {
                user.Errors.Add("Vai trò là bắt buộc");
            }
            else
            {
                var matchedRole = RoleMap.FirstOrDefault(r => 
                    r.Value.Equals(user.RoleName, StringComparison.OrdinalIgnoreCase));
                if (matchedRole.Key == 0)
                {
                    user.Errors.Add($"Vai trò '{user.RoleName}' không tồn tại");
                }
            }
        }

        private async Task LoadRolesAsync()
        {
            var roles = await _unitOfWork.RoleRepo.GetAllAsync();
            RoleList = new SelectList(roles, "RoleId", "RoleName");
            RoleMap = roles.ToDictionary(r => r.RoleId, r => r.RoleName);
        }
    }
}
