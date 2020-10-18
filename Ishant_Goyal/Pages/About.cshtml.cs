
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ishant_Goyal.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Ishant_Goyal.Pages
{
    public class AboutModel : PageModel
    {
        private readonly IshantContext _context;

        public AboutModel(IshantContext context)
        {
            _context = context;
        }

        public IList<Enrollment> Students { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<Enrollment> data =
                from student in _context.Student
                group student by student.EnrollmentDate into dateGroup
                select new Enrollment()
                {
                    EnrollmentDate = dateGroup.Key,
                    StudentCount = dateGroup.Count() 
                };

            Students = await data.AsNoTracking().ToListAsync();
        }
    }
}
