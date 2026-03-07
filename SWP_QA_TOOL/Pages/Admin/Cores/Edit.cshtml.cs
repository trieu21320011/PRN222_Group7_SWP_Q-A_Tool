using BussinessLayer.IServices;
using BussinessLayer.ViewModels.CoreDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SWP391_QA.Pages.Cores
{
    public class EditModel : PageModel
    {
        private readonly ICoreService _coreService;

        public EditModel(ICoreService coreService)
        {
            _coreService = coreService;
        }

        [BindProperty]
        public UpdateCoreDTO Core { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var core = await _coreService.GetCoreByIdAsync(id);

            if (core == null)
                return NotFound();

            Core = new UpdateCoreDTO
            {
                CoreId = core.CoreId,
                CoreCode = core.CoreCode,
                CoreName = core.CoreName
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _coreService.UpdateCoreAsync(Core);

            return RedirectToPage("Index");
        }
    }
}