using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ishant_Goyal.Models;

namespace Ishant_Goyal.Pages.Courses
{
    public class DetailsModel : PageModel
    {
        private readonly Ishant_Goyal.Models.IshantContext _context;

        public DetailsModel(Ishant_Goyal.Models.IshantContext context)
        {
            _context = context;
        }

        public Course Course { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Course = await _context.Course.FirstOrDefaultAsync(m => m.CourseId == id);

            if (Course == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
