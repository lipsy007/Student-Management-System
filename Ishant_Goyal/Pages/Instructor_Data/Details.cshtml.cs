using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ishant_Goyal.Models;

namespace Ishant_Goyal.Pages.Instructor_Data
{
    public class DetailsModel : PageModel
    {
        private readonly Ishant_Goyal.Models.IshantContext _context;

        public DetailsModel(Ishant_Goyal.Models.IshantContext context)
        {
            _context = context;
        }

        public Instructor Instructor { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Instructor = await _context.Instructor.FirstOrDefaultAsync(m => m.InstructorId == id);

            if (Instructor == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
