using BussinessLayer.IServices;
using BussinessLayer.ViewModels.TopicDTOs;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SWP_QA_TOOL.Pages.Admin.Topics
{
    public class IndexModel : PageModel
    {
        private readonly ITopicService _topicService;

        public IndexModel(ITopicService topicService)
        {
            _topicService = topicService;
        }

        public IList<GetTopicDTO> Topics { get; private set; } = new List<GetTopicDTO>();

        public async Task OnGetAsync()
        {
            var data = await _topicService.GetAllTopicsAsync();
            Topics = data?.ToList() ?? new List<GetTopicDTO>();
        }
    }
}
