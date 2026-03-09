using BussinessLayer.IServices;
using BussinessLayer.ViewModels.QuestionDTOs;
using BussinessLayer.ViewModels.TeamDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

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

            // Get team questions
            Questions = await _questionService.GetQuestionsByTeamAsync(id.Value) ?? new List<GetQuestionDTO>();

            return Page();
        }
    }
}
