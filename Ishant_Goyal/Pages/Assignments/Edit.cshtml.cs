using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ishant_Goyal.Models;

namespace Ishant_Goyal.Pages.Assignments
{
    public class EditModel : PageModel
    {
        private readonly Ishant_Goyal.Models.IshantContext _context;

        public EditModel(Ishant_Goyal.Models.IshantContext context)
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
           ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "CourseId");
           ViewData["InstructorId"] = new SelectList(_context.Instructor, "InstructorId", "InstructorName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(CourseAssignment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseAssignmentExists(CourseAssignment.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CourseAssignmentExists(int id)
        {
            return _context.CourseAssignment.Any(e => e.Id == id);
        }
    }
}
