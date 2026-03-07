using BussinessLayer.IServices;
using BussinessLayer.ViewModels.CoreDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SWP391_QA.Pages.Cores
{
    public class CreateModel : PageModel
    {
        private readonly ICoreService _coreService;

        public CreateModel(ICoreService coreService)
        {
            _coreService = coreService;
        }

        [BindProperty]
        public CreateCoreDTO Core { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _coreService.CreateCoreAsync(Core);

            return RedirectToPage("Index");
        }
    }
}