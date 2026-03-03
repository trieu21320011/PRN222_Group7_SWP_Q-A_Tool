using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SWP_QA_TOOL.Pages.Instructor
{
    [Authorize(Roles = "Instructor")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
