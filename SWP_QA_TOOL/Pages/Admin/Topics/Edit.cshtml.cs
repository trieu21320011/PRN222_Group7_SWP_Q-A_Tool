using BussinessLayer.IServices;
using BussinessLayer.ViewModels.TopicDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SWP_QA_TOOL.Pages.Admin.Topics
{
    public class EditModel : PageModel
    {
        private readonly ITopicService _topicService;

        public EditModel(ITopicService topicService)
        {
            _topicService = topicService;
        }

        [BindProperty]
        public UpdateTopicDTO Input { get; set; } = new UpdateTopicDTO();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var topic = await _topicService.GetTopicByIdAsync(id);
            if (topic == null) return NotFound();

            Input = new UpdateTopicDTO
            {
                TopicId = topic.TopicId,
                TopicCode = topic.TopicCode,
                TopicName = topic.TopicName,
                Description = topic.Description,
                Category = topic.Category,
                Difficulty = topic.Difficulty,
                MaxTeams = topic.MaxTeams,
                IsActive = topic.IsActive
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var updated = await _topicService.UpdateTopicAsync(Input);
            if (updated == null)
            {
                ModelState.AddModelError(string.Empty, "Cập nhật thất bại.");
                return Page();
            }

            TempData["Success"] = "Cập nhật đề tài thành công.";
            return RedirectToPage("./Index");
        }
    }
}
