using BussinessLayer.IServices;
using BussinessLayer.ViewModels.SemesterDTOs;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PresentationLayer.Areas.Admin.Pages.Semesters
{
    public class IndexModel : PageModel
    {
        private readonly ISemesterService _semesterService;

        public IndexModel(ISemesterService semesterService)
        {
            _semesterService = semesterService;
        }

        public IList<GetSemesterDTO> Semesters { get; private set; } = new List<GetSemesterDTO>();

        public async Task OnGetAsync()
        {
            var data = await _semesterService.GetAllSemestersAsync();
            Semesters = data?.ToList() ?? new List<GetSemesterDTO>();
        }
    }
}
