using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataLayer.DataLayer;

namespace SWP_QA_TOOL.Pages_Admin_Teams
{
    public class IndexModel : PageModel
    {
        private readonly DataLayer.DataLayer.SWP391QAContext _context;

        public IndexModel(DataLayer.DataLayer.SWP391QAContext context)
        {
            _context = context;
        }

        public IList<Team> Team { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Team = await _context.Teams
                .Include(t => t.Leader)
                .Include(t => t.Mentor).ToListAsync();
        }
    }
}
