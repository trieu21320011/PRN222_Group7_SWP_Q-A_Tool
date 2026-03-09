using BussinessLayer.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SWP_QA_TOOL.Pages.Admin.Reports
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly ISemesterService _semesterService;
        private readonly ICoreService _coreService;
        private readonly ITeamService _teamService;
        private readonly IQuestionService _questionService;
        private readonly IUserService _userService;

        public IndexModel(
            ISemesterService semesterService,
            ICoreService coreService,
            ITeamService teamService,
            IQuestionService questionService,
            IUserService userService)
        {
            _semesterService = semesterService;
            _coreService = coreService;
            _teamService = teamService;
            _questionService = questionService;
            _userService = userService;
        }

        [BindProperty(SupportsGet = true)]
        public int? SemesterId { get; set; }

        public SelectList SemesterList { get; set; } = null!;

        // Statistics
        public int TotalSemesters { get; set; }
        public int TotalCores { get; set; }
        public int TotalTeams { get; set; }
        public int TotalQuestions { get; set; }
        public int ResolvedQuestions { get; set; }
        public int PendingQuestions { get; set; }
        public int TotalUsers { get; set; }
        public int TotalInstructors { get; set; }
        public int TotalStudents { get; set; }

        // Core-level data
        public List<CoreReportItem> CoreReports { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            // Load semesters for filter
            var semesters = await _semesterService.GetAllSemestersAsync();
            SemesterList = new SelectList(semesters, "SemesterId", "SemesterCode");
            TotalSemesters = semesters?.Count() ?? 0;

            // Load all data
            var allCores = SemesterId.HasValue && SemesterId.Value > 0
                ? await _coreService.GetCoresBySemesterAsync(SemesterId.Value)
                : await _coreService.GetAllCoresAsync();

            TotalCores = allCores?.Count() ?? 0;

            var allTeams = await _teamService.GetAllTeamsAsync();
            TotalTeams = allTeams?.Count() ?? 0;

            var allQuestions = await _questionService.GetAllQuestionsAsync();
            TotalQuestions = allQuestions?.Count() ?? 0;
            ResolvedQuestions = allQuestions?.Count(q => q.Status == "Resolved" || q.IsClosed == true) ?? 0;
            PendingQuestions = TotalQuestions - ResolvedQuestions;

            var allUsers = await _userService.GetAllUsersAsync();
            TotalUsers = allUsers?.Count() ?? 0;
            TotalInstructors = allUsers?.Count(u => u.RoleName == "Instructor") ?? 0;
            TotalStudents = allUsers?.Count(u => u.RoleName == "Student") ?? 0;

            // Build core reports
            if (allCores != null)
            {
                foreach (var core in allCores)
                {
                    var coreTeams = allTeams?.Where(t => t.CoreName == core.CoreName).ToList();
                    var teamCount = coreTeams?.Count ?? 0;

                    CoreReports.Add(new CoreReportItem
                    {
                        CoreCode = core.CoreCode,
                        CoreName = core.CoreName,
                        SemesterCode = core.SemesterCode ?? "N/A",
                        InstructorName = core.InstructorName ?? "Chưa phân công",
                        TeamCount = teamCount,
                        MaxTeams = core.MaxTeams ?? 0,
                        IsActive = core.IsActive ?? false
                    });
                }
            }

            return Page();
        }

        public class CoreReportItem
        {
            public string CoreCode { get; set; } = null!;
            public string CoreName { get; set; } = null!;
            public string SemesterCode { get; set; } = null!;
            public string InstructorName { get; set; } = null!;
            public int TeamCount { get; set; }
            public int MaxTeams { get; set; }
            public bool IsActive { get; set; }
        }
    }
}
