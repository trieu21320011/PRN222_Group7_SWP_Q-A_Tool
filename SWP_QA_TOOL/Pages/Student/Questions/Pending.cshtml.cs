using BussinessLayer.IServices;
using BussinessLayer.ViewModels.QuestionDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace SWP_QA_TOOL.Pages.Student.Questions
{
    [Authorize(Roles = "Student")]
    public class PendingModel : PageModel
    {
        private readonly IQuestionService _questionService;

        public PendingModel(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        public IEnumerable<GetQuestionDTO> Questions { get; set; } = new List<GetQuestionDTO>();

        public async Task OnGetAsync()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(userIdClaim, out int userId))
            {
                var allQuestions = await _questionService.GetQuestionsByAuthorAsync(userId);
                // Filter for pending questions (Status = "Pending" or similar)
                Questions = allQuestions?.Where(q => q.Status == "Pending" || q.AnswerCount == 0) ?? new List<GetQuestionDTO>();
            }
        }
    }
}
