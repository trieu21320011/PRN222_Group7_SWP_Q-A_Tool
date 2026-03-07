using BussinessLayer.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Topics
{
    public class DeleteModel : PageModel
    {
        private readonly ITopicService _topicService;

        public DeleteModel(ITopicService topicService)
        {
            _topicService = topicService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            await _topicService.DeleteTopicAsync(id);
            return RedirectToPage("Index");
        }
    }
}