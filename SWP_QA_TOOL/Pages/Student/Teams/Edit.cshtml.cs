using BussinessLayer.IServices;
using BussinessLayer.ViewModels.TeamDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace SWP_QA_TOOL.Pages.Student.Teams
{
    [Authorize(Roles = "Student")]
    public class EditModel : PageModel
    {
        private readonly ITeamService _teamService;
        private readonly ICoreService _coreService;
        private readonly ITopicService _topicService;

        public EditModel(ITeamService teamService, ICoreService coreService, ITopicService topicService)
        {
            _teamService = teamService;
            _coreService = coreService;
            _topicService = topicService;
        }

        [BindProperty]
        public UpdateTeamDTO Team { get; set; } = new();

        public List<SelectListItem> Topics { get; set; } = new List<SelectListItem>();
        public string? CoreName { get; set; }
        public bool IsLeader { get; set; }

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

            var team = await _teamService.GetTeamByIdAsync(id.Value);
            if (team == null)
            {
                return NotFound();
            }

            // Check if user is the leader of this team
            var members = await _teamService.GetTeamMembersAsync(id.Value);
            IsLeader = members.Any(m => m.UserId == userId && m.Role == "Leader");

            if (!IsLeader)
            {
                TempData["ErrorMessage"] = "Bạn không có quyền chỉnh sửa nhóm này.";
                return RedirectToPage("/Student/Teams/MyTeam");
            }

            Team = new UpdateTeamDTO
            {
                TeamId = team.TeamId,
                TeamName = team.TeamName,
                TeamCode = team.TeamCode,
                Description = team.Description,
                ProjectName = team.ProjectName,
                IsActive = team.IsActive
            };

            CoreName = team.CoreName;
            await LoadDropdownsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return Page();
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return RedirectToPage("/Account/Login");
            }

            // Verify user is still the leader
            var members = await _teamService.GetTeamMembersAsync(Team.TeamId);
            if (!members.Any(m => m.UserId == userId && m.Role == "Leader"))
            {
                TempData["ErrorMessage"] = "Bạn không có quyền chỉnh sửa nhóm này.";
                return RedirectToPage("/Student/Teams/MyTeam");
            }

            try
            {
                var result = await _teamService.UpdateTeamAsync(Team);
                if (result != null)
                {
                    TempData["SuccessMessage"] = "Cập nhật nhóm thành công!";
                    return RedirectToPage("/Student/Teams/MyTeam");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Không thể cập nhật nhóm.");
                    await LoadDropdownsAsync();
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Có lỗi xảy ra: {ex.Message}");
                await LoadDropdownsAsync();
                return Page();
            }
        }

        private async Task LoadDropdownsAsync()
        {
            var topics = await _topicService.GetAllTopicsAsync();
            if (topics != null)
            {
                Topics = topics.Select(t => new SelectListItem
                {
                    Value = t.TopicId.ToString(),
                    Text = t.TopicName
                }).ToList();
            }
        }
    }
}
