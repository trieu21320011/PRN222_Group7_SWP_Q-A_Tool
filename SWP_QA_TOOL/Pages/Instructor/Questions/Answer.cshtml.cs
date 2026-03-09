using BussinessLayer.IServices;
using BussinessLayer.ViewModels.AnswerDTOs;
using BussinessLayer.ViewModels.QuestionDTOs;
using BussinessLayer.ViewModels.TeamDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace SWP_QA_TOOL.Pages.Instructor.Questions
{
    [Authorize(Roles = "Instructor,Mentor")]
    public class AnswerModel : PageModel
    {
        private readonly IQuestionService _questionService;
        private readonly IAnswerService _answerService;
        private readonly ITeamService _teamService;

        public AnswerModel(IQuestionService questionService, IAnswerService answerService, ITeamService teamService)
        {
            _questionService = questionService;
            _answerService = answerService;
            _teamService = teamService;
        }

        public GetQuestionDTO? Question { get; set; }
        public GetTeamDTO? Team { get; set; }
        public IEnumerable<GetAnswerDTO> Answers { get; set; } = new List<GetAnswerDTO>();
        public IEnumerable<GetQuestionDTO> OtherTeamQuestions { get; set; } = new List<GetQuestionDTO>();

        [BindProperty]
        public AnswerInput Input { get; set; } = new();

        public class AnswerInput
        {
            [Required(ErrorMessage = "Vui lòng nhập nội dung trả lời")]
            public string Content { get; set; } = string.Empty;
            public bool MarkResolved { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Question = await _questionService.GetQuestionByIdAsync(id.Value);
            if (Question == null)
            {
                return NotFound();
            }

            // Get team info by name if available
            if (!string.IsNullOrEmpty(Question.TeamName))
            {
                var allTeams = await _teamService.GetAllTeamsAsync();
                Team = allTeams?.FirstOrDefault(t => t.TeamName == Question.TeamName);
            }

            // Get existing answers
            Answers = await _answerService.GetAnswersByQuestionAsync(id.Value) ?? new List<GetAnswerDTO>();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                // Reload data
                await OnGetAsync(id);
                return Page();
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int userId = int.TryParse(userIdClaim, out var uid) ? uid : 0;

            // Create answer
            var createAnswer = new CreateAnswerDTO
            {
                QuestionId = id,
                Body = Input.Content,
                AuthorId = userId,
                IsMentorAnswer = true
            };

            await _answerService.CreateAnswerAsync(createAnswer);

            // Update question status using MarkAsResolvedAsync or UpdateQuestionAsync
            if (Input.MarkResolved)
            {
                await _questionService.MarkAsResolvedAsync(id, userId);
            }
            // Status will be updated automatically when an answer is added

            TempData["SuccessMessage"] = "Trả lời thành công!";
            return RedirectToPage("Answer", new { id = id });
        }

        public async Task<IActionResult> OnPostMarkResolvedAsync(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int userId = int.TryParse(userIdClaim, out var uid) ? uid : 0;
            
            await _questionService.MarkAsResolvedAsync(id, userId);
            TempData["SuccessMessage"] = "Đã đánh dấu giải quyết!";
            return RedirectToPage("Answer", new { id = id });
        }
    }
}
