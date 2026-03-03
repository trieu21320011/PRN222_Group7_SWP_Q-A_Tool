using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SWP_QA_TOOL.Pages.Admin.Users
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
