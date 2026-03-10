using BussinessLayer.IServices;
using BussinessLayer.ViewModels.CoreDTOs;
using BussinessLayer.ViewModels.TeamDTOs;
using BussinessLayer.ViewModels.TopicDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace SWP_QA_TOOL.Pages.Student.Teams
{
    [Authorize(Roles = "Student")]
    public class CreateModel : PageModel
    {
        private readonly ITeamService _teamService;
        private readonly ICoreService _coreService;
        private readonly ITopicService _topicService;

        public CreateModel(ITeamService teamService, ICoreService coreService, ITopicService topicService)
        {
            _teamService = teamService;
            _coreService = coreService;
            _topicService = topicService;
        }

        [BindProperty]
        public CreateTeamDTO Team { get; set; } = new();

        public List<SelectListItem> Cores { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Topics { get; set; } = new List<SelectListItem>();

        public async Task<IActionResult> OnGetAsync()
        {
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

            // Set the current user as leader
            Team.LeaderId = userId;
            Team.IsActive = true;

            // Generate a unique team code
            Team.TeamCode = $"TEAM-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

            try
            {
                var createdTeam = await _teamService.CreateTeamAsync(Team);
                
                // Add the creator as a leader member
                await _teamService.AddMemberToTeamAsync(createdTeam.TeamId, userId, "Leader");

                TempData["SuccessMessage"] = "Tạo nhóm thành công!";
                return RedirectToPage("/Student/Teams/MyTeam");
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
            var cores = await _coreService.GetAllCoresAsync();
            if (cores != null)
            {
                Cores = cores.Select(c => new SelectListItem
                {
                    Value = c.CoreId.ToString(),
                    Text = c.CoreName
                }).ToList();
            }

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
