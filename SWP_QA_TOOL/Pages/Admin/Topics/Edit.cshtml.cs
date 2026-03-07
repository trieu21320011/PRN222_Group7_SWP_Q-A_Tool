using BussinessLayer.IServices;
using BussinessLayer.ViewModels.TopicDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Topics
{
    public class EditModel : PageModel
    {
        private readonly ITopicService _topicService;

        public EditModel(ITopicService topicService)
        {
            _topicService = topicService;
        }

        [BindProperty]
        public UpdateTopicDTO Topic { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var topic = await _topicService.GetTopicByIdAsync(id);

            if (topic == null)
                return NotFound();

            Topic = new UpdateTopicDTO
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
            await _topicService.UpdateTopicAsync(Topic);
            return RedirectToPage("Index");
        }
    }
}