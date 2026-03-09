using BussinessLayer.IServices;
using BussinessLayer.ViewModels.CoreDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SWP_QA_TOOL.Pages.Admin.Cores
{
    public class DeleteModel : PageModel
    {
        private readonly ICoreService _coreService;

        public DeleteModel(ICoreService coreService)
        {
            _coreService = coreService;
        }

        public GetCoreDTO? Core { get; private set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Core = await _coreService.GetCoreByIdAsync(id);
            if (Core == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var ok = await _coreService.DeleteCoreAsync(id);
            if (!ok)
            {
                TempData["Error"] = "Xóa thất bại (có thể đang bị ràng buộc dữ liệu).";
                return RedirectToPage("./Index");
            }

            TempData["Success"] = "Xóa Core thành công.";
            return RedirectToPage("./Index");
        }
    }
}
