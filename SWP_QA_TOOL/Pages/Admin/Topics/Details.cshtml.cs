using BussinessLayer.IServices;
using BussinessLayer.ViewModels.TopicDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Topics
{
    public class DetailsModel : PageModel
    {
        private readonly ITopicService _topicService;

        public DetailsModel(ITopicService topicService)
        {
            _topicService = topicService;
        }

        public GetTopicDTO Topic { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Topic = await _topicService.GetTopicWithTeamsAndCoresAsync(id);

            if (Topic == null)
                return NotFound();

            return Page();
        }
    }
}