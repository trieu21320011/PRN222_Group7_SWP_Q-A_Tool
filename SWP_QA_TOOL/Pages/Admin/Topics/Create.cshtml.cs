using BussinessLayer.IServices;
using BussinessLayer.ViewModels.TopicDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Topics
{
    public class CreateModel : PageModel
    {
        private readonly ITopicService _topicService;

        public CreateModel(ITopicService topicService)
        {
            _topicService = topicService;
        }

        [BindProperty]
        public CreateTopicDTO Topic { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _topicService.CreateTopicAsync(Topic);

            return RedirectToPage("Index");
        }
    }
}