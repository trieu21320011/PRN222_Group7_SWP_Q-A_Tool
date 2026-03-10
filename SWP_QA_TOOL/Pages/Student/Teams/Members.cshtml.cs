using BussinessLayer.IServices;
using BussinessLayer.ViewModels.TeamDTOs;
using BussinessLayer.ViewModels.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace SWP_QA_TOOL.Pages.Student.Teams
{
    [Authorize(Roles = "Student")]
    public class MembersModel : PageModel
    {
        private readonly ITeamService _teamService;
        private readonly IUserService _userService;

        public MembersModel(ITeamService teamService, IUserService userService)
        {
            _teamService = teamService;
            _userService = userService;
        }

        public GetTeamDTO? Team { get; set; }
        public IList<TeamMemberDTO> TeamMembers { get; set; } = new List<TeamMemberDTO>();
        public int CurrentUserId { get; set; }
        public bool IsLeader { get; set; }

        [BindProperty]
        public string? SearchEmail { get; set; }

        public IList<GetUserDTO>? SearchResults { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return RedirectToPage("/Account/Login");
            }

            CurrentUserId = userId;

            Team = await _teamService.GetTeamByIdAsync(id.Value);
            if (Team == null)
            {
                return NotFound();
            }

            var members = await _teamService.GetTeamMembersAsync(id.Value);
            TeamMembers = members?.ToList() ?? new List<TeamMemberDTO>();

            IsLeader = TeamMembers.Any(m => m.UserId == userId && m.Role == "Leader");

            if (!IsLeader)
            {
                TempData["ErrorMessage"] = "Bạn không có quyền quản lý thành viên nhóm này.";
                return RedirectToPage("/Student/Teams/MyTeam");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostSearchAsync(int id)
        {
            await LoadTeamDataAsync(id);

            if (!string.IsNullOrEmpty(SearchEmail))
            {
                var allUsers = await _userService.GetAllUsersAsync();
                SearchResults = allUsers
                    .Where(u => u.Email != null && u.Email.Contains(SearchEmail, StringComparison.OrdinalIgnoreCase))
                    .Where(u => !TeamMembers.Any(m => m.UserId == u.UserId))
                    .Take(10)
                    .ToList();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddMemberAsync(int id, int userId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int currentUserId))
            {
                return RedirectToPage("/Account/Login");
            }

            // Verify user is the leader
            var members = await _teamService.GetTeamMembersAsync(id);
            if (!members.Any(m => m.UserId == currentUserId && m.Role == "Leader"))
            {
                TempData["ErrorMessage"] = "Bạn không có quyền thêm thành viên.";
                return RedirectToPage("/Student/Teams/Members", new { id });
            }

            try
            {
                await _teamService.AddMemberToTeamAsync(id, userId, "Member");
                TempData["SuccessMessage"] = "Thêm thành viên thành công!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Không thể thêm thành viên: {ex.Message}";
            }

            return RedirectToPage("/Student/Teams/Members", new { id });
        }

        public async Task<IActionResult> OnPostRemoveMemberAsync(int id, int userId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int currentUserId))
            {
                return RedirectToPage("/Account/Login");
            }

            // Verify user is the leader
            var members = await _teamService.GetTeamMembersAsync(id);
            if (!members.Any(m => m.UserId == currentUserId && m.Role == "Leader"))
            {
                TempData["ErrorMessage"] = "Bạn không có quyền xóa thành viên.";
                return RedirectToPage("/Student/Teams/Members", new { id });
            }

            // Cannot remove leader
            if (userId == currentUserId)
            {
                TempData["ErrorMessage"] = "Không thể xóa nhóm trưởng khỏi nhóm.";
                return RedirectToPage("/Student/Teams/Members", new { id });
            }

            try
            {
                await _teamService.RemoveMemberFromTeamAsync(id, userId);
                TempData["SuccessMessage"] = "Xóa thành viên thành công!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Không thể xóa thành viên: {ex.Message}";
            }

            return RedirectToPage("/Student/Teams/Members", new { id });
        }

        private async Task LoadTeamDataAsync(int teamId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdClaim, out int userId))
            {
                CurrentUserId = userId;
            }

            Team = await _teamService.GetTeamByIdAsync(teamId);
            var members = await _teamService.GetTeamMembersAsync(teamId);
            TeamMembers = members?.ToList() ?? new List<TeamMemberDTO>();
            IsLeader = TeamMembers.Any(m => m.UserId == CurrentUserId && m.Role == "Leader");
        }
    }
}
