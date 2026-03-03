using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SWP_QA_TOOL.Pages.Student.Questions
{
    [Authorize(Roles = "Student")]
    public class CreateModel : PageModel
    {
        public void OnGet()
        {
        }

        public void OnPost()
        {
        }
    }
}
