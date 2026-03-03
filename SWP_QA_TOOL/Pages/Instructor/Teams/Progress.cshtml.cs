using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SWP_QA_TOOL.Pages.Instructor.Teams
{
    [Authorize(Roles = "Instructor")]
    public class ProgressModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
