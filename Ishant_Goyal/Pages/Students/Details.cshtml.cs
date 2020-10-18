using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ishant_Goyal.Models;

namespace Ishant_Goyal.Pages.Students
{
    public class DetailsModel : PageModel
    {
        private readonly Ishant_Goyal.Models.IshantContext _context;

        public DetailsModel(Ishant_Goyal.Models.IshantContext context)
        {
            _context = context;
        }

        public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student = await _context.Student
                .Include(s => s.CityCodeNavigation)
                .Include(s => s.StateCodeNavigation).FirstOrDefaultAsync(m => m.Id == id);

            if (Student == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
