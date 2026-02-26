using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataLayer.DataLayer;

namespace SWP_QA_TOOL.Pages_Admin_Teams
{
    public class CreateModel : PageModel
    {
        private readonly DataLayer.DataLayer.SWP391QAContext _context;

        public CreateModel(DataLayer.DataLayer.SWP391QAContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["LeaderId"] = new SelectList(_context.Users, "UserId", "Email");
        ViewData["MentorId"] = new SelectList(_context.Users, "UserId", "Email");
            return Page();
        }

        [BindProperty]
        public Team Team { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Teams.Add(Team);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
