using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SWP_QA_TOOL.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            // Redirect to login if not authenticated
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                return RedirectToPage("/Account/Login");
            }

            // Redirect based on role
            if (User.IsInRole("Admin"))
            {
                return RedirectToPage("/Admin/Index");
            }
            else if (User.IsInRole("Instructor") || User.IsInRole("Mentor"))
            {
                return RedirectToPage("/Instructor/Index");
            }
            else if (User.IsInRole("Student"))
            {
                return RedirectToPage("/Student/Index");
            }

            return Page();
        }
    }
}
