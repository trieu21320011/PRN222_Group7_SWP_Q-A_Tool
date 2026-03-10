using BussinessLayer.IServices;
using BussinessLayer.ViewModels.AnswerDTOs;
using BussinessLayer.ViewModels.CommentDTOs;
using BussinessLayer.ViewModels.QuestionDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace SWP_QA_TOOL.Pages.Student.Questions
{
    [Authorize(Roles = "Student")]
    public class DetailsModel : PageModel
    {
        private readonly IQuestionService _questionService;
        private readonly IAnswerService _answerService;
        private readonly ICommentService _commentService;

        public DetailsModel(IQuestionService questionService, IAnswerService answerService, ICommentService commentService)
        {
            _questionService = questionService;
            _answerService = answerService;
            _commentService = commentService;
        }

        public GetQuestionDTO? Question { get; set; }
        public IList<GetAnswerDTO> Answers { get; set; } = new List<GetAnswerDTO>();
        public IList<GetCommentDTO> QuestionComments { get; set; } = new List<GetCommentDTO>();
        public Dictionary<int, IList<GetCommentDTO>> AnswerComments { get; set; } = new Dictionary<int, IList<GetCommentDTO>>();
        public int CurrentUserId { get; set; }
        public bool IsAuthor { get; set; }

        [BindProperty]
        public string? NewComment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return RedirectToPage("/Account/Login");
            }

            CurrentUserId = userId;

            Question = await _questionService.GetQuestionByIdAsync(id.Value);
            if (Question == null)
            {
                return NotFound();
            }

            // Check if current user is the author
            IsAuthor = Question.AuthorName != null;

            // Get answers for this question
            var answers = await _answerService.GetAnswersByQuestionAsync(id.Value);
            Answers = answers?.ToList() ?? new List<GetAnswerDTO>();

            // Get comments for question
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

        public async Task<IActionResult> OnPostMarkAsResolvedAsync(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return RedirectToPage("/Account/Login");
            }

            try
            {
                await _questionService.MarkAsResolvedAsync(id, userId);
                TempData["SuccessMessage"] = "Câu hỏi đã được đánh dấu là giải quyết!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Có lỗi xảy ra: {ex.Message}";
            }

            return RedirectToPage(new { id });
        }

        public async Task<IActionResult> OnPostAddQuestionCommentAsync(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return RedirectToPage("/Account/Login");
            }

            if (string.IsNullOrWhiteSpace(NewComment))
            {
                TempData["ErrorMessage"] = "Vui lòng nhập nội dung câu hỏi thêm.";
                return RedirectToPage(new { id });
            }

            try
            {
                var createCommentDTO = new CreateCommentDTO
                {
                    Body = NewComment,
                    AuthorId = userId,
                    QuestionId = id,
                    AnswerId = null
                };

                await _commentService.CreateCommentAsync(createCommentDTO);
                TempData["SuccessMessage"] = "Đã gửi câu hỏi thêm thành công!";
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                TempData["ErrorMessage"] = $"Có lỗi xảy ra: {innerMessage}";
            }

            return RedirectToPage(new { id });
        }

        public async Task<IActionResult> OnPostAddAnswerCommentAsync(int id, int answerId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return RedirectToPage("/Account/Login");
            }

            if (string.IsNullOrWhiteSpace(NewComment))
            {
                TempData["ErrorMessage"] = "Vui lòng nhập nội dung câu hỏi thêm.";
                return RedirectToPage(new { id });
            }

            try
            {
                var createCommentDTO = new CreateCommentDTO
                {
                    Body = NewComment,
                    AuthorId = userId,
                    QuestionId = null,  // Comment on answer doesn't need QuestionId
                    AnswerId = answerId
                };

                await _commentService.CreateCommentAsync(createCommentDTO);
                TempData["SuccessMessage"] = "Đã gửi câu hỏi thêm thành công!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Có lỗi xảy ra: {ex.Message}";
            }

            return RedirectToPage(new { id });
        }
    }
}
