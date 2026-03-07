using BussinessLayer.IServices;
using BussinessLayer.ViewModels.CoreDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SWP391_QA.Pages.Cores
{
    public class DeleteModel : PageModel
    {
        private readonly ICoreService _coreService;

        public DeleteModel(ICoreService coreService)
        {
            _coreService = coreService;
        }

        [BindProperty]
        public GetCoreDTO Core { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Core = await _coreService.GetCoreByIdAsync(id);

            if (Core == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _coreService.DeleteCoreAsync(id);

            return RedirectToPage("Index");
        }
    }
}