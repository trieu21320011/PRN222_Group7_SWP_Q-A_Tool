using BussinessLayer.IServices;
using BussinessLayer.ViewModels.CoreDTOs;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SWP391_QA.Pages.Cores
{
    public class DetailsModel : PageModel
    {
        private readonly ICoreService _coreService;

        public DetailsModel(ICoreService coreService)
        {
            _coreService = coreService;
        }

        public GetCoreDTO Core { get; set; }

        public async Task OnGetAsync(int id)
        {
            Core = await _coreService.GetCoreWithTeamsAsync(id);
        }
    }
}