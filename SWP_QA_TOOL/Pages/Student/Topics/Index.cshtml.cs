using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SWP_QA_TOOL.Pages.Student.Topics
{
    [Authorize(Roles = "Student")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
