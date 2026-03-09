using BussinessLayer.IServices;
using BussinessLayer.ViewModels.CoreDTOs;
using BussinessLayer.ViewModels.TeamDTOs;
using BussinessLayer.ViewModels.QuestionDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace SWP_QA_TOOL.Pages.Instructor.Teams
{
    [Authorize(Roles = "Instructor,Mentor")]
    public class ProgressModel : PageModel
    {
        private readonly ITeamService _teamService;
        private readonly ICoreService _coreService;
        private readonly IQuestionService _questionService;

        public ProgressModel(ITeamService teamService, ICoreService coreService, IQuestionService questionService)
        {
            _teamService = teamService;
            _coreService = coreService;
            _questionService = questionService;
        }

        public IEnumerable<GetTeamDTO> Teams { get; set; } = new List<GetTeamDTO>();
        public IEnumerable<GetCoreDTO> Cores { get; set; } = new List<GetCoreDTO>();
        public Dictionary<int, int> TeamPendingQuestions { get; set; } = new Dictionary<int, int>();
        public int? SelectedCoreId { get; set; }

        public async Task<IActionResult> OnGetAsync(int? coreId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int instructorId = int.TryParse(userIdClaim, out var id) ? id : 0;

            SelectedCoreId = coreId;

            // Get instructor's cores
            Cores = await _coreService.GetCoresByInstructorAsync(instructorId) ?? new List<GetCoreDTO>();

            // Get teams from instructor's cores
            var allTeams = new List<GetTeamDTO>();
            foreach (var core in Cores)
            {
                if (!coreId.HasValue || core.CoreId == coreId.Value)
                {
                    var teamsInCore = await _teamService.GetTeamsByCoreAsync(core.CoreId);
                    if (teamsInCore != null)
                        allTeams.AddRange(teamsInCore);
                }
            }
            Teams = allTeams;

            // Get pending questions count for each team
            var allQuestions = await _questionService.GetQuestionsAssignedToInstructorAsync(instructorId);
            if (allQuestions != null)
            {
                foreach (var team in Teams)
                {
                    var pendingCount = allQuestions.Count(q => q.TeamName == team.TeamName && q.Status == "Open");
                    TeamPendingQuestions[team.TeamId] = pendingCount;
                }
            }

            return Page();
        }

        public int GetPendingQuestionCount(int teamId)
        {
            return TeamPendingQuestions.TryGetValue(teamId, out var count) ? count : 0;
        }
    }
}
