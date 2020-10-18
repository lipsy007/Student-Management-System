using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ishant_Goyal.Models;

namespace Ishant_Goyal.Pages.Instructor_Data
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
            return Page();
        }

        [BindProperty]
        public Instructor Instructor { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var CourseExist = _context.Instructor.Where(c => c.InstructorName.Equals(Instructor.InstructorName)).FirstOrDefault();
            if (CourseExist != null)
            {
                Msg = "The Instructorr Name Exist already ";
                 return Page();
            }

            _context.Instructor.Add(Instructor);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
