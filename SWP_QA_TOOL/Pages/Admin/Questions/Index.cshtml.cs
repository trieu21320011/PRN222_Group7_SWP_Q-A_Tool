using BussinessLayer.IServices;
using BussinessLayer.ViewModels.QuestionDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SWP_QA_TOOL.Pages.Admin.Questions
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly IQuestionService _questionService;
        private readonly ISemesterService _semesterService;
        private readonly ICoreService _coreService;
        private readonly IUserService _userService;

        public IndexModel(IQuestionService questionService, ISemesterService semesterService, ICoreService coreService, IUserService userService)
        {
            _questionService = questionService;
            _semesterService = semesterService;
            _coreService = coreService;
            _userService = userService;
        }

        public IEnumerable<GetQuestionDTO> Questions { get; set; } = new List<GetQuestionDTO>();

        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? StatusFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SemesterFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? CoreFilter { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string? TeamFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? InstructorFilter { get; set; }

        public IEnumerable<string> Semesters { get; set; } = new List<string>();
        public IEnumerable<string> Cores { get; set; } = new List<string>();
        public IEnumerable<string> Teams { get; set; } = new List<string>();
        public IEnumerable<string> Instructors { get; set; } = new List<string>();

        // Statistics
        public int TotalQuestions { get; set; }
        public int AnsweredQuestions { get; set; }
        public int UnansweredQuestions { get; set; }

        public async Task OnGetAsync()
        {
            var questions = await _questionService.GetAllQuestionsAsync();

            // Populate filter dropdowns from existing data for simplicity
            Semesters = questions.Where(q => !string.IsNullOrEmpty(q.SemesterCode)).Select(q => q.SemesterCode!).Distinct().OrderBy(s => s);
            Cores = questions.Where(q => !string.IsNullOrEmpty(q.CoreName)).Select(q => q.CoreName!).Distinct().OrderBy(c => c);
            Teams = questions.Where(q => !string.IsNullOrEmpty(q.TeamName)).Select(q => q.TeamName!).Distinct().OrderBy(t => t);
            Instructors = questions.Where(q => !string.IsNullOrEmpty(q.AssignedInstructorName)).Select(q => q.AssignedInstructorName!).Distinct().OrderBy(i => i);

            // Apply Filters
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                questions = questions.Where(q => 
                    (q.Title != null && q.Title.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)) ||
                    (q.AuthorName != null && q.AuthorName.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                );
            }

            if (!string.IsNullOrEmpty(StatusFilter))
            {
                if (StatusFilter == "Answered")
                {
                    questions = questions.Where(q => q.AnswerCount > 0);
                }
                else if (StatusFilter == "Unanswered")
                {
                    questions = questions.Where(q => q.AnswerCount == 0);
                }
            }
            
            if (!string.IsNullOrEmpty(CoreFilter))
            {
                questions = questions.Where(q => q.CoreName == CoreFilter);
            }

            if (!string.IsNullOrEmpty(TeamFilter))
            {
                questions = questions.Where(q => q.TeamName == TeamFilter);
            }

            if (!string.IsNullOrEmpty(InstructorFilter))
            {
                questions = questions.Where(q => q.AssignedInstructorName == InstructorFilter);
            }

            Questions = questions.OrderByDescending(q => q.CreatedAt).ToList();

            // Calculate Statistics for UI
            TotalQuestions = Questions.Count();
            AnsweredQuestions = Questions.Count(q => q.AnswerCount > 0);
            UnansweredQuestions = TotalQuestions - AnsweredQuestions;
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _questionService.DeleteQuestionAsync(id);
            return RedirectToPage();
        }
    }
}
