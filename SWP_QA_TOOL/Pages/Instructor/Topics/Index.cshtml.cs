using BussinessLayer.IServices;
using BussinessLayer.ViewModels.SemesterDTOs;
using BussinessLayer.ViewModels.TopicDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace SWP_QA_TOOL.Pages.Instructor.Topics
{
    [Authorize(Roles = "Instructor,Mentor")]
    public class IndexModel : PageModel
    {
        private readonly ITopicService _topicService;
        private readonly ISemesterService _semesterService;

        public IndexModel(ITopicService topicService, ISemesterService semesterService)
        {
            _topicService = topicService;
            _semesterService = semesterService;
        }

        public IEnumerable<GetTopicDTO> Topics { get; set; } = new List<GetTopicDTO>();
        public IEnumerable<GetSemesterDTO> Semesters { get; set; } = new List<GetSemesterDTO>();
        public int? SelectedSemesterId { get; set; }
        public string? SearchTerm { get; set; }

        public async Task OnGetAsync(int? semesterId, string? search)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int instructorId = int.TryParse(userIdClaim, out var id) ? id : 0;

            SelectedSemesterId = semesterId;
            SearchTerm = search;

            // Get all semesters for filter dropdown
            Semesters = await _semesterService.GetAllSemestersAsync() ?? new List<GetSemesterDTO>();

            // Get instructor's topics
            var myTopics = await _topicService.GetTopicsByInstructorAsync(instructorId);
            
            // Apply filters
            if (myTopics != null)
            {
                var filteredTopics = myTopics.AsEnumerable();
                
                // SemesterId not available in GetTopicDTO, skip semester filter
                
                if (!string.IsNullOrEmpty(search))
                {
                    filteredTopics = filteredTopics.Where(t => 
                        t.TopicName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                        (t.Description?.Contains(search, StringComparison.OrdinalIgnoreCase) ?? false));
                }
                
                Topics = filteredTopics.ToList();
            }
        }
    }
}
