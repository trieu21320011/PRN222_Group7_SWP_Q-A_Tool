using BussinessLayer.IServices;
using BussinessLayer.ViewModels.TopicDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SWP_QA_TOOL.Pages.Admin.Topics
{
    public class DeleteModel : PageModel
    {
        private readonly ITopicService _topicService;

        public DeleteModel(ITopicService topicService)
        {
            _topicService = topicService;
        }

        public GetTopicDTO? Topic { get; private set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Topic = await _topicService.GetTopicByIdAsync(id);
            if (Topic == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var ok = await _topicService.DeleteTopicAsync(id);
            if (!ok)
            {
                TempData["Error"] = "Xóa thất bại (có thể đang bị ràng buộc dữ liệu).";
                return RedirectToPage("./Index");
            }

            TempData["Success"] = "Xóa đề tài thành công.";
            return RedirectToPage("./Index");
        }
    }
}
