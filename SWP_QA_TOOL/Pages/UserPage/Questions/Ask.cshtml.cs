using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SWP_QA_TOOL.Pages.UserPage.Questions
{
    public class AskModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            // Will implement later
            return RedirectToPage("/UserPage/Questions/Index");
        }
    }
}
