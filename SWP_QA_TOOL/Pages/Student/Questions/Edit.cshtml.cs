using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SWP_QA_TOOL.Pages.Student.Questions
{
    [Authorize(Roles = "Student")]
    public class EditModel : PageModel
    {
        public void OnGet(int? id)
        {
        }

        public void OnPost()
        {
        }
    }
}
