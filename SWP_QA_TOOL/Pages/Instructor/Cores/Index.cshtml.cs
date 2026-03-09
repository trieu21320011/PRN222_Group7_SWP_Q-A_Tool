using BussinessLayer.IServices;
using BussinessLayer.ViewModels.CoreDTOs;
using BussinessLayer.ViewModels.TopicDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace SWP_QA_TOOL.Pages.Instructor.Cores
{
    [Authorize(Roles = "Instructor,Mentor")]
    public class IndexModel : PageModel
    {
        private readonly ICoreService _coreService;
        private readonly ITopicService _topicService;

        public IndexModel(ICoreService coreService, ITopicService topicService)
        {
            _coreService = coreService;
            _topicService = topicService;
        }

        public IEnumerable<GetCoreDTO> Cores { get; set; } = new List<GetCoreDTO>();
        public IEnumerable<GetTopicDTO> Topics { get; set; } = new List<GetTopicDTO>();
        public string? SearchTerm { get; set; }

        public async Task OnGetAsync(string? search)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int instructorId = int.TryParse(userIdClaim, out var id) ? id : 0;

            SearchTerm = search;

            // Get instructor's topics for filter
            Topics = await _topicService.GetTopicsByInstructorAsync(instructorId) ?? new List<GetTopicDTO>();

            // Get instructor's cores
            var myCores = await _coreService.GetCoresByInstructorAsync(instructorId);
            
            if (myCores != null)
            {
                var filteredCores = myCores.AsEnumerable();
                
                if (!string.IsNullOrEmpty(search))
                {
                    filteredCores = filteredCores.Where(c => 
                        c.CoreName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                        (c.CoreCode?.Contains(search, StringComparison.OrdinalIgnoreCase) ?? false));
                }
                
                Cores = filteredCores.ToList();
            }
        }
    }
}
