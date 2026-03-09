using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SWP_QA_TOOL.Pages.Instructor.Questions
{
    [Authorize(Roles = "Instructor,Mentor")]
    public class AnsweredModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Redirect to Index with Answered status filter
            return RedirectToPage("Index", new { status = "Answered" });
        }
    }
}
