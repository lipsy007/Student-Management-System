using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ishant_Goyal.Models;

namespace Ishant_Goyal.Pages.Assignments
{
    public class DeleteModel : PageModel
    {
        private readonly Ishant_Goyal.Models.IshantContext _context;

        public DeleteModel(Ishant_Goyal.Models.IshantContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CourseAssignment CourseAssignment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CourseAssignment = await _context.CourseAssignment
                .Include(c => c.Course)
                .Include(c => c.Instructor).FirstOrDefaultAsync(m => m.Id == id);

            if (CourseAssignment == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CourseAssignment = await _context.CourseAssignment.FindAsync(id);

            if (CourseAssignment != null)
            {
                _context.CourseAssignment.Remove(CourseAssignment);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
