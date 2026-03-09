using BussinessLayer.IServices;
using BussinessLayer.ViewModels.TeamDTOs;
using BussinessLayer.ViewModels.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SWP_QA_TOOL.Pages.Admin.Users
{
    [Authorize(Roles = "Admin")]
    public class DetailsModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly ITeamService _teamService;

        public DetailsModel(IUserService userService, ITeamService teamService)
        {
            _userService = userService;
            _teamService = teamService;
        }

        public GetUserDTO UserDetails { get; set; } = new GetUserDTO();
        public IEnumerable<GetTeamDTO> UserTeams { get; set; } = new List<GetTeamDTO>();

        [BindProperty]
        public string TeamCode { get; set; } = string.Empty;

        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            UserDetails = user;
            UserTeams = await _teamService.GetTeamsByUserAsync(id);

            return Page();
        }

        public async Task<IActionResult> OnPostAssignTeamAsync(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            UserDetails = user;
            UserTeams = await _teamService.GetTeamsByUserAsync(id);

            if (string.IsNullOrEmpty(TeamCode))
            {
                ErrorMessage = "Vui lòng nhập mã nhóm (Team Code).";
                return Page();
            }

            var team = await _teamService.GetTeamByCodeAsync(TeamCode);
            if (team == null)
            {
                ErrorMessage = $"Không tìm thấy nhóm với mã '{TeamCode}'.";
                return Page();
            }

            var success = await _teamService.AddMemberToTeamAsync(team.TeamId, id, "Member");
            if (success)
            {
                SuccessMessage = $"Đã gán học sinh vào nhóm '{team.TeamName}' thành công.";
                UserTeams = await _teamService.GetTeamsByUserAsync(id);
                ModelState.Clear();
                TeamCode = string.Empty;
            }
            else
            {
                ErrorMessage = "Học sinh này đã nằm trong nhóm tương ứng hoặc có lỗi hệ thống xảy ra.";
            }

            return Page();
        }
    }
}
