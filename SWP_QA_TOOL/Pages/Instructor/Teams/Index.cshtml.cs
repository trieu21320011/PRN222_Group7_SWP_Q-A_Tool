using BussinessLayer.IServices;
using BussinessLayer.ViewModels.CoreDTOs;
using BussinessLayer.ViewModels.TeamDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace SWP_QA_TOOL.Pages.Instructor.Teams
{
    [Authorize(Roles = "Instructor,Mentor")]
    public class IndexModel : PageModel
    {
        private readonly ITeamService _teamService;
        private readonly ICoreService _coreService;

        public IndexModel(ITeamService teamService, ICoreService coreService)
        {
            _teamService = teamService;
            _coreService = coreService;
        }

        public IEnumerable<GetTeamDTO> Teams { get; set; } = new List<GetTeamDTO>();
        public IEnumerable<GetCoreDTO> Cores { get; set; } = new List<GetCoreDTO>();
        public int? SelectedCoreId { get; set; }
        public string? SearchTerm { get; set; }

        public async Task OnGetAsync(int? coreId, string? search)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int instructorId = int.TryParse(userIdClaim, out var id) ? id : 0;

            SelectedCoreId = coreId;
            SearchTerm = search;

            // Get instructor's cores for filter
            Cores = await _coreService.GetCoresByInstructorAsync(instructorId) ?? new List<GetCoreDTO>();

            // Get teams from instructor's cores
            var allTeams = new List<GetTeamDTO>();
            
            if (coreId.HasValue)
            {
                // Filter by specific core
                var teamsInCore = await _teamService.GetTeamsByCoreAsync(coreId.Value);
                if (teamsInCore != null)
                    allTeams.AddRange(teamsInCore);
            }
            else
            {
                // Get teams from all instructor's cores
                foreach (var core in Cores)
                {
                    var teamsInCore = await _teamService.GetTeamsByCoreAsync(core.CoreId);
                    if (teamsInCore != null)
                        allTeams.AddRange(teamsInCore);
                }
            }
            
            // Apply search filter
            if (!string.IsNullOrEmpty(search))
            {
                allTeams = allTeams.Where(t => 
                    t.TeamName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    (t.LeaderName?.Contains(search, StringComparison.OrdinalIgnoreCase) ?? false)).ToList();
            }
            
            Teams = allTeams;
        }
    }
}
