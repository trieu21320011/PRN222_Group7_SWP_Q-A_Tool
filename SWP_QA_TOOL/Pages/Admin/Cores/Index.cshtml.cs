using BussinessLayer.IServices;
using BussinessLayer.ViewModels.CoreDTOs;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SWP_QA_TOOL.Pages.Admin.Cores
{
    public class IndexModel : PageModel
    {
        private readonly ICoreService _coreService;

        public IndexModel(ICoreService coreService)
        {
            _coreService = coreService;
        }

        public IList<GetCoreDTO> Cores { get; private set; } = new List<GetCoreDTO>();

        public async Task OnGetAsync()
        {
            var data = await _coreService.GetAllCoresAsync();
            Cores = data?.ToList() ?? new List<GetCoreDTO>();
        }
    }
}
