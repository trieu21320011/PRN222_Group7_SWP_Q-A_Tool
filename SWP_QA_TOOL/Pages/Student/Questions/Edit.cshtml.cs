using BussinessLayer.IServices;
using BussinessLayer.ViewModels.QuestionDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace SWP_QA_TOOL.Pages.Student.Questions
{
    [Authorize(Roles = "Student")]
    public class EditModel : PageModel
    {
        private readonly IQuestionService _questionService;
        private readonly ITeamService _teamService;
        private readonly ICoreService _coreService;

        public EditModel(IQuestionService questionService, ITeamService teamService, ICoreService coreService)
        {
            _questionService = questionService;
            _teamService = teamService;
            _coreService = coreService;
        }

        [BindProperty]
        public UpdateQuestionDTO Question { get; set; } = new();

        public List<SelectListItem> Teams { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Cores { get; set; } = new List<SelectListItem>();

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

            var question = await _questionService.GetQuestionByIdAsync(id.Value);
            if (question == null)
            {
                return NotFound();
            }

            // Check if question is closed
            if (question.IsClosed == true)
            {
                TempData["ErrorMessage"] = "Không thể chỉnh sửa câu hỏi đã được giải quyết.";
                return RedirectToPage("/Student/Questions/Details", new { id });
            }

            Question = new UpdateQuestionDTO
            {
                QuestionId = question.QuestionId,
                Title = question.Title,
                Body = question.Body,
                Excerpt = question.Excerpt,
                Category = question.Category,
                Difficulty = question.Difficulty,
                IsPrivate = question.IsPrivate ?? false
            };

            await LoadDropdownsAsync(userId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return RedirectToPage("/Account/Login");
            }

            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync(userId);
                return Page();
            }

            try
            {
                var result = await _questionService.UpdateQuestionAsync(Question);
                if (result != null)
                {
                    TempData["SuccessMessage"] = "Cập nhật câu hỏi thành công!";
                    return RedirectToPage("/Student/Questions/Details", new { id = Question.QuestionId });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Không thể cập nhật câu hỏi.");
                    await LoadDropdownsAsync(userId);
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Có lỗi xảy ra: {ex.Message}");
                await LoadDropdownsAsync(userId);
                return Page();
            }
        }

        private async Task LoadDropdownsAsync(int userId)
        {
            var teams = await _teamService.GetTeamsByUserAsync(userId);
            if (teams != null)
            {
                Teams = teams.Select(t => new SelectListItem
                {
                    Value = t.TeamId.ToString(),
                    Text = t.TeamName
                }).ToList();
            }

            var cores = await _coreService.GetCoresByUserAsync(userId);
            if (cores != null)
            {
                Cores = cores.Select(c => new SelectListItem
                {
                    Value = c.CoreId.ToString(),
                    Text = c.CoreName
                }).ToList();
            }
        }
    }
}
