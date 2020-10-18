using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ishant_Goyal.Models;

namespace Ishant_Goyal.Pages.Students
{
    public class EditModel : PageModel
    {
        private readonly Ishant_Goyal.Models.IshantContext _context;

        public EditModel(Ishant_Goyal.Models.IshantContext context)
        {
            _context = context;
        }

        [BindProperty]
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
           ViewData["CityCode"] = new SelectList(_context.City, "CityCode", "CityName");
           ViewData["StateCode"] = new SelectList(_context.State, "StateCode", "StateName");
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

            _context.Attach(Student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(Student.Id))
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

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.Id == id);
        }
    }
}
