using BussinessLayer.IServices;
using BussinessLayer.ViewModels.CoreDTOs;
using BussinessLayer.ViewModels.TeamDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SWP_QA_TOOL.Pages.Instructor.Cores
{
    [Authorize(Roles = "Instructor,Mentor")]
    public class DetailsModel : PageModel
    {
        private readonly ICoreService _coreService;
        private readonly ITeamService _teamService;

        public DetailsModel(ICoreService coreService, ITeamService teamService)
        {
            _coreService = coreService;
            _teamService = teamService;
        }

        public GetCoreDTO? Core { get; set; }
        public IEnumerable<GetTeamDTO> Teams { get; set; } = new List<GetTeamDTO>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Core = await _coreService.GetCoreByIdAsync(id.Value);
            if (Core == null)
            {
                return NotFound();
            }

            // Get teams in this core
            Teams = await _teamService.GetTeamsByCoreAsync(id.Value) ?? new List<GetTeamDTO>();

            return Page();
        }
    }
}
