using BussinessLayer.IServices;
using BussinessLayer.ViewModels.CoreDTOs;
using BussinessLayer.ViewModels.TeamDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SWP_QA_TOOL.Pages.Admin.Cores
{
    public class DetailsModel : PageModel
    {
        private readonly ICoreService _coreService;
        private readonly ITeamService _teamService;

        public DetailsModel(ICoreService coreService, ITeamService teamService)
        {
            _coreService = coreService;
            _teamService = teamService;
        }

        public GetCoreDTO? Core { get; private set; }
        public IList<GetTeamDTO> Teams { get; private set; } = new List<GetTeamDTO>();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Core = await _coreService.GetCoreByIdAsync(id);
            if (Core == null) return NotFound();

            var teams = await _teamService.GetTeamsByCoreAsync(id);
            Teams = teams?.ToList() ?? new List<GetTeamDTO>();

            return Page();
        }
    }
}
