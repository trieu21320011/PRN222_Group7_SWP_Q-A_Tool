using BussinessLayer.IServices;
using BussinessLayer.ViewModels.SemesterDTOs;
using BussinessLayer.ViewModels.TopicDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace SWP_QA_TOOL.Pages.Instructor.Semesters
{
    [Authorize(Roles = "Instructor,Mentor")]
    public class DetailsModel : PageModel
    {
        private readonly ISemesterService _semesterService;
        private readonly ITopicService _topicService;

        public DetailsModel(ISemesterService semesterService, ITopicService topicService)
        {
            _semesterService = semesterService;
            _topicService = topicService;
        }

        public GetSemesterDTO? Semester { get; set; }
        public IEnumerable<GetTopicDTO> Topics { get; set; } = new List<GetTopicDTO>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Semester = await _semesterService.GetSemesterByIdAsync(id.Value);
            if (Semester == null)
            {
                return NotFound();
            }

            // Get topics for this semester that belong to the instructor
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int instructorId = int.TryParse(userIdClaim, out var uid) ? uid : 0;
            
            var myTopics = await _topicService.GetTopicsByInstructorAsync(instructorId);
            // Filter topics by the semester (topics don't have SemesterId in GetTopicDTO, so we show all)
            Topics = myTopics ?? new List<GetTopicDTO>();

            return Page();
        }
    }
}
