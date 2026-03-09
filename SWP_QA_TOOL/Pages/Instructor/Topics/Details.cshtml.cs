using BussinessLayer.IServices;
using BussinessLayer.ViewModels.CoreDTOs;
using BussinessLayer.ViewModels.TopicDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace SWP_QA_TOOL.Pages.Instructor.Topics
{
    [Authorize(Roles = "Instructor,Mentor")]
    public class DetailsModel : PageModel
    {
        private readonly ITopicService _topicService;
        private readonly ICoreService _coreService;

        public DetailsModel(ITopicService topicService, ICoreService coreService)
        {
            _topicService = topicService;
            _coreService = coreService;
        }

        public GetTopicDTO? Topic { get; set; }
        public IEnumerable<GetCoreDTO> Cores { get; set; } = new List<GetCoreDTO>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Topic = await _topicService.GetTopicByIdAsync(id.Value);
            if (Topic == null)
            {
                return NotFound();
            }

            // Get instructor's cores (filtered by SemesterCode)
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int instructorId = int.TryParse(userIdClaim, out var uid) ? uid : 0;
            var myCores = await _coreService.GetCoresByInstructorAsync(instructorId);
            Cores = myCores?.Where(c => c.SemesterCode == Topic.SemesterCode).ToList() ?? new List<GetCoreDTO>();

            return Page();
        }
    }
}
