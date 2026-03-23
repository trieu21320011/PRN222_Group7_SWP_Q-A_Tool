using BussinessLayer.IServices;
using BussinessLayer.ViewModels.QuestionDTOs;
using BussinessLayer.ViewModels.TeamDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace SWP_QA_TOOL.Pages.Instructor.Teams
{
    [Authorize(Roles = "Instructor,Mentor")]
    public class DetailsModel : PageModel
    {
        private readonly ITeamService _teamService;
        private readonly IQuestionService _questionService;

        public DetailsModel(ITeamService teamService, IQuestionService questionService)
        {
            _teamService = teamService;
            _questionService = questionService;
        }

        public GetTeamDTO? Team { get; set; }
        public IEnumerable<GetQuestionDTO> Questions { get; set; } = new List<GetQuestionDTO>();
        public int OpenQuestionCount => Questions.Count(q => q.Status == "Open");
        public int AnsweredQuestionCount => Questions.Count(q => q.Status == "Answered");
        public int ResolvedQuestionCount => Questions.Count(q => q.Status == "Resolved");
        public int CurrentUserId { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            CurrentUserId = int.TryParse(userIdClaim, out var uid) ? uid : 0;

            // Get team with members
            Team = await _teamService.GetTeamWithMembersAsync(id.Value);
            if (Team == null)
            {
                Team = await _teamService.GetTeamByIdAsync(id.Value);
            }
            
            if (Team == null)
            {
                return NotFound();
            }

            // Get team questions, filtered by the team's topic if available
            var allTeamQuestions = await _questionService.GetQuestionsByTeamAsync(id.Value) ?? new List<GetQuestionDTO>();

            if (Team.TopicId.HasValue)
            {
                // Only show questions that belong to this team's topic
                Questions = allTeamQuestions.Where(q => q.TopicId == Team.TopicId.Value).ToList();
            }
            else
            {
                Questions = allTeamQuestions.ToList();
            }

            return Page();
        }
    }
}
