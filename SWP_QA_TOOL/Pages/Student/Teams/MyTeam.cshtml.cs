using BussinessLayer.IServices;
using BussinessLayer.ViewModels.TeamDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace SWP_QA_TOOL.Pages.Student.Teams
{
    [Authorize(Roles = "Student")]
    public class MyTeamModel : PageModel
    {
        private readonly ITeamService _teamService;

        public MyTeamModel(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public GetTeamDTO? MyTeam { get; set; }
        public IList<TeamMemberDTO> TeamMembers { get; set; } = new List<TeamMemberDTO>();
        public int CurrentUserId { get; set; }
        public bool IsLeader { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return RedirectToPage("/Account/Login");
            }

            CurrentUserId = userId;

            // Get user's teams
            var teams = await _teamService.GetTeamsByUserAsync(userId);
            MyTeam = teams?.FirstOrDefault();

            if (MyTeam != null)
            {
                // Get team members
                var members = await _teamService.GetTeamMembersAsync(MyTeam.TeamId);
                TeamMembers = members?.ToList() ?? new List<TeamMemberDTO>();

                // Check if current user is the leader
                IsLeader = TeamMembers.Any(m => m.UserId == userId && m.Role == "Leader");
            }

            return Page();
        }
    }
}
