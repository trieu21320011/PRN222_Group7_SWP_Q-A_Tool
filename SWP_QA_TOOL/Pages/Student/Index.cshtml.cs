using BussinessLayer.IServices;
using BussinessLayer.ViewModels.CoreDTOs;
using BussinessLayer.ViewModels.QuestionDTOs;
using BussinessLayer.ViewModels.TeamDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace SWP_QA_TOOL.Pages.Student
{
    [Authorize(Roles = "Student")]
    public class IndexModel : PageModel
    {
        private readonly ICoreService _coreService;
        private readonly ITeamService _teamService;
        private readonly IQuestionService _questionService;

        public IndexModel(ICoreService coreService, ITeamService teamService, IQuestionService questionService)
        {
            _coreService = coreService;
            _teamService = teamService;
            _questionService = questionService;
        }

        public IList<GetCoreDTO> MyCores { get; set; } = new List<GetCoreDTO>();
        public IList<GetTeamDTO> MyTeams { get; set; } = new List<GetTeamDTO>();
        public IList<GetQuestionDTO> MyQuestions { get; set; } = new List<GetQuestionDTO>();
        public IList<GetQuestionDTO> PendingQuestions { get; set; } = new List<GetQuestionDTO>();
        public IList<GetQuestionDTO> AnsweredQuestions { get; set; } = new List<GetQuestionDTO>();

        public int CurrentUserId { get; set; }

        public async Task OnGetAsync()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdClaim, out int userId))
            {
                CurrentUserId = userId;

                // Load user's cores
                var cores = await _coreService.GetCoresByUserAsync(userId);
                MyCores = cores?.ToList() ?? new List<GetCoreDTO>();

                // Load user's teams
                var teams = await _teamService.GetTeamsByUserAsync(userId);
                MyTeams = teams?.ToList() ?? new List<GetTeamDTO>();

                // Load user's questions
                var questions = await _questionService.GetQuestionsByAuthorAsync(userId);
                MyQuestions = questions?.ToList() ?? new List<GetQuestionDTO>();

                // Separate pending and answered questions
                PendingQuestions = MyQuestions.Where(q => q.AnswerCount == 0 || q.AnswerCount == null).ToList();
                AnsweredQuestions = MyQuestions.Where(q => q.AnswerCount > 0).ToList();
            }
        }
    }
}
