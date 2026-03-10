using BussinessLayer.IServices;
using BussinessLayer.ViewModels.CoreDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace SWP_QA_TOOL.Pages.Student.Cores
{
    [Authorize(Roles = "Student")]
    public class IndexModel : PageModel
    {
        private readonly ICoreService _coreService;

        public IndexModel(ICoreService coreService)
        {
            _coreService = coreService;
        }

        public IEnumerable<GetCoreDTO> MyCores { get; set; } = new List<GetCoreDTO>();

        public async Task OnGetAsync()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(userIdClaim, out int userId))
            {
                MyCores = await _coreService.GetCoresByUserAsync(userId) ?? new List<GetCoreDTO>();
            }
        }
    }
}
