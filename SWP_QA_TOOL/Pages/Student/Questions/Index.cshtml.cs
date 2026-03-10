using BussinessLayer.IServices;
using BussinessLayer.ViewModels.QuestionDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace SWP_QA_TOOL.Pages.Student.Questions
{
    [Authorize(Roles = "Student")]
    public class IndexModel : PageModel
    {
        private readonly IQuestionService _questionService;
        private readonly ITeamService _teamService;

        public IndexModel(IQuestionService questionService, ITeamService teamService)
        {
            _questionService = questionService;
            _teamService = teamService;
        }

        public IList<GetQuestionDTO> MyQuestions { get; set; } = new List<GetQuestionDTO>();
        public int CurrentUserId { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return RedirectToPage("/Account/Login");
            }

            CurrentUserId = userId;

            // Get user's questions
            var questions = await _questionService.GetQuestionsByAuthorAsync(userId);
            MyQuestions = questions?.ToList() ?? new List<GetQuestionDTO>();

            return Page();
        }
    }
}
