using BussinessLayer.IServices;
using BussinessLayer.ViewModels.TeamDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace SWP_QA_TOOL.Pages.Student.Topics
{
    [Authorize(Roles = "Student")]
    public class IndexModel : PageModel
    {
        private readonly ITeamService _teamService;

        public IndexModel(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public IEnumerable<GetTeamDTO> MyTeams { get; set; } = new List<GetTeamDTO>();

        public async Task OnGetAsync()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdClaim, out int userId))
            {
                MyTeams = await _teamService.GetTeamsByUserAsync(userId);
            }
        }
    }
}
