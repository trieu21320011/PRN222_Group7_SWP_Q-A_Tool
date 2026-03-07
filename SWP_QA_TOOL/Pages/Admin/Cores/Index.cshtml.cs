using BussinessLayer.IServices;
using BussinessLayer.ViewModels.CoreDTOs;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SWP391_QA.Pages.Cores
{
    public class IndexModel : PageModel
    {
        private readonly ICoreService _coreService;

        public IndexModel(ICoreService coreService)
        {
            _coreService = coreService;
        }

        public IEnumerable<GetCoreDTO> Cores { get; set; }

        public async Task OnGetAsync()
        {
            Cores = await _coreService.GetAllCoresAsync();
        }
    }
}