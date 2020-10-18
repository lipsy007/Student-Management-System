using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ishant_Goyal.Models;

namespace Ishant_Goyal.Pages.Courses
{
    public class CreateModel : PageModel
    {
        private readonly Ishant_Goyal.Models.IshantContext _context;

        public CreateModel(Ishant_Goyal.Models.IshantContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Course Course { get; set; }
        public string Msg { get; private set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var CourseExit = _context.Course.Where(c => c.CourseName.Equals(Course.CourseName)).FirstOrDefault();

            if (CourseExit != null)
            {
                Msg = "The Course Exist already";
                return Page();
            }

            _context.Course.Add(Course);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
