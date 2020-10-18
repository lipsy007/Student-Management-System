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
    public class IndexModel : PageModel
    {
        private readonly Ishant_Goyal.Models.IshantContext _context;

        public IndexModel(Ishant_Goyal.Models.IshantContext context)
        {
            _context = context;
        }

        public IList<CourseAssignment> CourseAssignment { get;set; }

        public async Task OnGetAsync()
        {
            CourseAssignment = await _context.CourseAssignment
                .Include(c => c.Course)
                .Include(c => c.Instructor).ToListAsync();
        }
    }
}
