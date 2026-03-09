using BussinessLayer.IServices;
using BussinessLayer.ViewModels.CoreDTOs;
using BussinessLayer.ViewModels.QuestionDTOs;
using BussinessLayer.ViewModels.TeamDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace SWP_QA_TOOL.Pages.Instructor
{
    [Authorize(Roles = "Instructor,Mentor")]
    public class IndexModel : PageModel
    {
        private readonly ISemesterService _semesterService;
        private readonly ICoreService _coreService;
        private readonly ITopicService _topicService;
        private readonly ITeamService _teamService;
        private readonly IQuestionService _questionService;

        public IndexModel(
            ISemesterService semesterService,
            ICoreService coreService,
            ITopicService topicService,
            ITeamService teamService,
            IQuestionService questionService)
        {
            _semesterService = semesterService;
            _coreService = coreService;
            _topicService = topicService;
            _teamService = teamService;
            _questionService = questionService;
        }

        public int ActiveSemesterCount { get; set; }
        public int TopicCount { get; set; }
        public int CoreCount { get; set; }
        public int TeamCount { get; set; }
        public int PendingQuestionCount { get; set; }
        public IEnumerable<GetQuestionDTO> RecentQuestions { get; set; } = new List<GetQuestionDTO>();
        public IEnumerable<GetTeamDTO> RecentTeams { get; set; } = new List<GetTeamDTO>();
        public IEnumerable<GetCoreDTO> MyCores { get; set; } = new List<GetCoreDTO>();

        public async Task OnGetAsync()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int instructorId = int.TryParse(userIdClaim, out var id) ? id : 0;

            // Get stats
            var activeSemesters = await _semesterService.GetActiveSemestersAsync();
            ActiveSemesterCount = activeSemesters?.Count() ?? 0;

            var myCores = await _coreService.GetCoresByInstructorAsync(instructorId);
            MyCores = myCores ?? new List<GetCoreDTO>();
            CoreCount = MyCores.Count();

            var myTopics = await _topicService.GetTopicsByInstructorAsync(instructorId);
            TopicCount = myTopics?.Count() ?? 0;

            // Get teams from instructor's cores
            var allTeams = new List<GetTeamDTO>();
            foreach (var core in MyCores)
            {
                var teamsInCore = await _teamService.GetTeamsByCoreAsync(core.CoreId);
                if (teamsInCore != null)
                    allTeams.AddRange(teamsInCore);
            }
            TeamCount = allTeams.Count;
            RecentTeams = allTeams.Take(5);

            // Get pending questions
            var assignedQuestions = await _questionService.GetQuestionsAssignedToInstructorAsync(instructorId);
            var pendingQuestions = assignedQuestions?.Where(q => q.Status == "Open" || q.Status == "Answered").ToList() ?? new List<GetQuestionDTO>();
            PendingQuestionCount = pendingQuestions.Count;
            RecentQuestions = pendingQuestions.OrderByDescending(q => q.CreatedAt).Take(5);
        }
    }
}
