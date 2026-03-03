using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SWP_QA_TOOL.Pages.Student.Teams
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
