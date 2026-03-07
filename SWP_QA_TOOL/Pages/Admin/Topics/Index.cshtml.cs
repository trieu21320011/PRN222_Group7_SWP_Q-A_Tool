using BussinessLayer.IServices;
using BussinessLayer.ViewModels.TopicDTOs;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Topics
{
    public class IndexModel : PageModel
    {
        private readonly ITopicService _topicService;

        public IndexModel(ITopicService topicService)
        {
            _topicService = topicService;
        }

        public IEnumerable<GetTopicDTO> Topics { get; set; }

        public async Task OnGetAsync()
        {
            Topics = await _topicService.GetAllTopicsAsync();
        }
    }
}