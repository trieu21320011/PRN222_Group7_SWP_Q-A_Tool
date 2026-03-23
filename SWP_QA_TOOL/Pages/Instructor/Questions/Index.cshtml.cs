using BussinessLayer.IServices;
using BussinessLayer.ViewModels.CoreDTOs;
using BussinessLayer.ViewModels.QuestionDTOs;
using BussinessLayer.ViewModels.TeamDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace SWP_QA_TOOL.Pages.Instructor.Questions
{
    [Authorize(Roles = "Instructor,Mentor")]
    public class IndexModel : PageModel
    {
        private readonly IQuestionService _questionService;
        private readonly ICoreService _coreService;
        private readonly ITeamService _teamService;

        public IndexModel(IQuestionService questionService, ICoreService coreService, ITeamService teamService)
        {
            _questionService = questionService;
            _coreService = coreService;
            _teamService = teamService;
        }

        public IEnumerable<GetQuestionDTO> Questions { get; set; } = new List<GetQuestionDTO>();
        public IEnumerable<GetCoreDTO> Cores { get; set; } = new List<GetCoreDTO>();
        public IEnumerable<GetTeamDTO> Teams { get; set; } = new List<GetTeamDTO>();
        
        public int? SelectedCoreId { get; set; }
        public string? SelectedTeamName { get; set; }
        public string? SelectedStatus { get; set; }
        public string? SearchTerm { get; set; }
        public int? FilteredTeamId { get; set; }
        public int? FilteredTopicId { get; set; }
        public int CurrentUserId { get; set; }

        public int OpenCount => Questions.Count(q => q.Status == "Open");
        public int AnsweredCount => Questions.Count(q => q.Status == "Answered");
        public int ResolvedCount => Questions.Count(q => q.Status == "Resolved");

        public async Task OnGetAsync(int? coreId, string? teamName, string? status, string? search, int? teamId, int? topicId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            CurrentUserId = int.TryParse(userIdClaim, out var id) ? id : 0;

            SelectedCoreId = coreId;
            SelectedTeamName = teamName;
            SelectedStatus = status;
            SearchTerm = search;
            FilteredTeamId = teamId;
            FilteredTopicId = topicId;

            // Get instructor's cores for filter dropdown
            Cores = await _coreService.GetCoresByInstructorAsync(CurrentUserId) ?? new List<GetCoreDTO>();

            // Get teams from instructor's cores
            var allTeams = new List<GetTeamDTO>();
            foreach (var core in Cores)
            {
                var teamsInCore = await _teamService.GetTeamsByCoreAsync(core.CoreId);
                if (teamsInCore != null)
                    allTeams.AddRange(teamsInCore);
            }
            Teams = allTeams;

            IEnumerable<GetQuestionDTO> filteredQuestions;

            // If coming from team details with a specific teamId, show all questions for that team
            if (teamId.HasValue)
            {
                filteredQuestions = await _questionService.GetQuestionsByTeamAsync(teamId.Value) ?? new List<GetQuestionDTO>();

                // Further filter by topicId if provided
                if (topicId.HasValue)
                {
                    filteredQuestions = filteredQuestions.Where(q => q.TopicId == topicId.Value).ToList();
                }
            }
            else
            {
                // Default: questions assigned to this instructor
                filteredQuestions = await _questionService.GetQuestionsAssignedToInstructorAsync(CurrentUserId) ?? new List<GetQuestionDTO>();

                // Apply filters by team name
                if (!string.IsNullOrEmpty(teamName))
                {
                    filteredQuestions = filteredQuestions.Where(q => q.TeamName == teamName).ToList();
                }
                else if (coreId.HasValue)
                {
                    var core = Cores.FirstOrDefault(c => c.CoreId == coreId.Value);
                    if (core != null)
                    {
                        var teamNamesInCore = allTeams.Where(t => t.CoreName == core.CoreName).Select(t => t.TeamName).ToHashSet();
                        filteredQuestions = filteredQuestions.Where(q => !string.IsNullOrEmpty(q.TeamName) && teamNamesInCore.Contains(q.TeamName)).ToList();
                    }
                }
            }

            if (!string.IsNullOrEmpty(status))
            {
                filteredQuestions = filteredQuestions.Where(q => q.Status == status).ToList();
            }

            if (!string.IsNullOrEmpty(search))
            {
                filteredQuestions = filteredQuestions.Where(q =>
                    q.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    (q.Body?.Contains(search, StringComparison.OrdinalIgnoreCase) ?? false)).ToList();
            }

            Questions = filteredQuestions.OrderByDescending(q => q.CreatedAt).ToList();
        }
    }
}
