using BussinessLayer.IServices;
using BussinessLayer.ViewModels.QuestionDTOs;
using BussinessLayer.ViewModels.TeamDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace SWP_QA_TOOL.Pages.Student.Questions
{
    [Authorize(Roles = "Student")]
    public class CreateModel : PageModel
    {
        private readonly IQuestionService _questionService;
        private readonly ITeamService _teamService;
        private readonly ICoreService _coreService;

        public CreateModel(IQuestionService questionService, ITeamService teamService, ICoreService coreService)
        {
            _questionService = questionService;
            _teamService = teamService;
            _coreService = coreService;
        }

        [BindProperty]
        public CreateQuestionDTO Question { get; set; } = new();

        public List<SelectListItem> Teams { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Cores { get; set; } = new List<SelectListItem>();
        public int CurrentUserId { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return RedirectToPage("/Account/Login");
            }

            CurrentUserId = userId;
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

            Question.AuthorId = userId;

            try
            {
                var createdQuestion = await _questionService.CreateQuestionAsync(Question);
                TempData["SuccessMessage"] = "Tạo câu hỏi thành công!";
                return RedirectToPage("/Student/Questions/Details", new { id = createdQuestion.QuestionId });
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
