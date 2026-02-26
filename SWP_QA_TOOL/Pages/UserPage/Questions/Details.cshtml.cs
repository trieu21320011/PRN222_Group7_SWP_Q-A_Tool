using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SWP_QA_TOOL.Pages.UserPage.Questions
{
    public class DetailsModel : PageModel
    {
        public int Id { get; set; }

        public void OnGet(int? id)
        {
            Id = id ?? 0;
        }
    }
}
