using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataLayer.DataLayer;

namespace SWP_QA_TOOL.Pages_Admin_Meetings
{
    public class IndexModel : PageModel
    {
        private readonly DataLayer.DataLayer.SWP391QAContext _context;

        public IndexModel(DataLayer.DataLayer.SWP391QAContext context)
        {
            _context = context;
        }

        public IList<Meeting> Meeting { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Meeting = await _context.Meetings
                .Include(m => m.Organizer)
                .Include(m => m.Team).ToListAsync();
        }
    }
}
