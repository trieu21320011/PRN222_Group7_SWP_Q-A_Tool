using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SWP_QA_TOOL.Pages.Instructor.Questions
{
    [Authorize(Roles = "Instructor")]
    public class AnsweredModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
