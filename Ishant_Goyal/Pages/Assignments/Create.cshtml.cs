using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ishant_Goyal.Models;

namespace Ishant_Goyal.Pages.Assignments
{
    public class CreateModel : PageModel
    {
        private readonly Ishant_Goyal.Models.IshantContext _context;

        public CreateModel(Ishant_Goyal.Models.IshantContext context)
        {
            _context = context;
        }
        public string Msg { get; set; }
        public IActionResult OnGet()
        {
        ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "CourseName");
        ViewData["InstructorId"] = new SelectList(_context.Instructor, "InstructorId", "InstructorName");
            return Page();
        }

        [BindProperty]
        public CourseAssignment CourseAssignment { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var CourseExit = _context.CourseAssignment.Where(c => c.InstructorId.Equals
            (CourseAssignment.InstructorId)).FirstOrDefault();

            if (CourseExit != null)
            {
                Msg = "The Instructor Name Exist already";
                ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "CourseName");
                ViewData["InstructorId"] = new SelectList(_context.Instructor, "InstructorId", "InstructorName");
                return Page();
            }
            _context.CourseAssignment.Add(CourseAssignment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
