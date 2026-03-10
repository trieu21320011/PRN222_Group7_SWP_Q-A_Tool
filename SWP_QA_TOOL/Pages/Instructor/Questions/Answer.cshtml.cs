using BussinessLayer.IServices;
using BussinessLayer.ViewModels.AnswerDTOs;
using BussinessLayer.ViewModels.CommentDTOs;
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
        private readonly ICommentService _commentService;

        public AnswerModel(IQuestionService questionService, IAnswerService answerService, ITeamService teamService, ICommentService commentService)
        {
            _questionService = questionService;
            _answerService = answerService;
            _teamService = teamService;
            _commentService = commentService;
        }

        public GetQuestionDTO? Question { get; set; }
        public GetTeamDTO? Team { get; set; }
        public IEnumerable<GetAnswerDTO> Answers { get; set; } = new List<GetAnswerDTO>();
        public IEnumerable<GetQuestionDTO> OtherTeamQuestions { get; set; } = new List<GetQuestionDTO>();
        public IList<GetCommentDTO> QuestionComments { get; set; } = new List<GetCommentDTO>();
        public Dictionary<int, IList<GetCommentDTO>> AnswerComments { get; set; } = new Dictionary<int, IList<GetCommentDTO>>();

        [BindProperty]
        public AnswerInput Input { get; set; } = new();

        [BindProperty]
        public string? NewComment { get; set; }

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
            var answersResult = await _answerService.GetAnswersByQuestionAsync(id.Value);
            Answers = answersResult ?? new List<GetAnswerDTO>();

            // Get comments for question (follow-up questions)
            var questionComments = await _commentService.GetCommentsByQuestionAsync(id.Value);
            QuestionComments = questionComments?.ToList() ?? new List<GetCommentDTO>();

            // Get comments for each answer
            foreach (var answer in Answers)
            {
                var answerComments = await _commentService.GetCommentsByAnswerAsync(answer.AnswerId);
                AnswerComments[answer.AnswerId] = answerComments?.ToList() ?? new List<GetCommentDTO>();
            }

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

        public async Task<IActionResult> OnPostAddQuestionCommentAsync(int questionId, string commentBody)
        {
            if (string.IsNullOrWhiteSpace(commentBody))
            {
                TempData["ErrorMessage"] = "Vui lòng nhập nội dung phản hồi.";
                return RedirectToPage(new { id = questionId });
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return RedirectToPage("/Account/Login");
            }

            try
            {
                var createComment = new CreateCommentDTO
                {
                    Body = commentBody,
                    AuthorId = userId,
                    QuestionId = questionId,
                    AnswerId = null
                };

                await _commentService.CreateCommentAsync(createComment);
                TempData["SuccessMessage"] = "Đã gửi phản hồi thành công!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi: {ex.InnerException?.Message ?? ex.Message}";
            }

            return RedirectToPage(new { id = questionId });
        }

        public async Task<IActionResult> OnPostAddAnswerCommentAsync(int questionId, int answerId, string commentBody)
        {
            if (string.IsNullOrWhiteSpace(commentBody))
            {
                TempData["ErrorMessage"] = "Vui lòng nhập nội dung phản hồi.";
                return RedirectToPage(new { id = questionId });
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return RedirectToPage("/Account/Login");
            }

            try
            {
                var createComment = new CreateCommentDTO
                {
                    Body = commentBody,
                    AuthorId = userId,
                    QuestionId = null,
                    AnswerId = answerId
                };

                await _commentService.CreateCommentAsync(createComment);
                TempData["SuccessMessage"] = "Đã gửi phản hồi thành công!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi: {ex.InnerException?.Message ?? ex.Message}";
            }

            return RedirectToPage(new { id = questionId });
        }
    }
}
