using BussinessLayer.IServices;
using BussinessLayer.ViewModels.TopicDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SWP_QA_TOOL.Pages.Admin.Topics
{
    public class CreateModel : PageModel
    {
        private readonly ITopicService _topicService;
        private readonly ISemesterService _semesterService;

        public CreateModel(ITopicService topicService, ISemesterService semesterService)
        {
            _topicService = topicService;
            _semesterService = semesterService;
        }

        [BindProperty]
        public CreateTopicDTO Input { get; set; } = new CreateTopicDTO
        {
            IsActive = true
        };

        public SelectList SemesterList { get; set; } = null!;

        public async Task OnGetAsync()
        {
            await LoadSemestersAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadSemestersAsync();
                return Page();
            }

            var created = await _topicService.CreateTopicAsync(Input);
            if (created == null || created.TopicId <= 0)
            {
                ModelState.AddModelError(string.Empty, "Tạo đề tài thất bại.");
                await LoadSemestersAsync();
                return Page();
            }

            TempData["Success"] = "Tạo đề tài thành công.";
            return RedirectToPage("./Index");
        }

        private async Task LoadSemestersAsync()
        {
            var semesters = await _semesterService.GetAllSemestersAsync();
            SemesterList = new SelectList(semesters, "SemesterId", "SemesterName");
        }
    }
}
