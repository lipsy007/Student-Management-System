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
    public class IndexModel : PageModel
    {
        private readonly Ishant_Goyal.Models.IshantContext _context;

        public IndexModel(Ishant_Goyal.Models.IshantContext context)
        {
            _context = context;
        }

        public IList<Course> Course { get;set; }

        public async Task OnGetAsync()
        {
            Course = await _context.Course.ToListAsync();
        }
    }
}
