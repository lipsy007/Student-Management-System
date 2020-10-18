using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Ishant_Goyal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Ishant_Goyal
{
    public class DisplayModel : PageModel
    {
        private readonly Ishant_Goyal.Models.IshantContext _context;

        public DisplayModel(Ishant_Goyal.Models.IshantContext context)
        {
            _context = context;
        }
        public string searchInstructor { get; set; }
        public string searchCourse { get; set; }
        public SelectList INS { get; set; }
        public SelectList Cou { get; set; }
        public List<Student> Student { get; set; }
        public CourseAssignment val { get; set; }
        public int c { get; set; }
        public string a { get; set; }
                
        public async Task OnGetAsync()
        {
            
            IQueryable<string> INSQuery = from m in _context.CourseAssignment.Include(c => c.Instructor)
                                          orderby m.Instructor.InstructorName, m.Course.CourseName
                                          select m.Instructor.InstructorName;
            IQueryable<string> COUQuery = from m in _context.CourseAssignment.Include(c => c.Course)
                                          orderby m.Course.CourseName
                                          select m.Course.CourseName;
            IQueryable<Student> studentsIQ = from s in _context.Student.Include(c => c.Course).Include(d => d.StateCodeNavigation).Include(e => e.CityCodeNavigation)
                                             select s;

            INS = new SelectList(await INSQuery.Distinct().ToListAsync());
            Cou = new SelectList(await COUQuery.Distinct().ToListAsync());
            Student = await studentsIQ.ToListAsync();

        }
        public async Task<IActionResult> OnPostAsync(String searchInstructor, string searchCourse)
        {
            
            IQueryable<string> INSQuery = from m in _context.CourseAssignment.Include(c => c.Instructor)
                                          orderby m.Instructor.InstructorName, m.Course.CourseName
                                          select m.Instructor.InstructorName;
            IQueryable<string> COUQuery = from m in _context.CourseAssignment.Include(c => c.Course)
                                          orderby m.Course.CourseName
                                          select m.Course.CourseName;


            IQueryable<Student> studentsIQ = from s in _context.Student.Include(c => c.Course)

                                             select s;
            
            IQueryable<CourseAssignment> Courseassignmen = from s in _context.CourseAssignment.Include(c => c.Course).Include(c=>c.Instructor)

                                             select s;

            INS = new SelectList(await INSQuery.Distinct().ToListAsync());
            Cou = new SelectList(await COUQuery.Distinct().ToListAsync());

            if (!string.IsNullOrEmpty(searchCourse) && !string.IsNullOrEmpty(searchInstructor))
            {
               var r = Courseassignmen.Where(x => x.Course.CourseName == searchCourse && x.Instructor.InstructorName == searchInstructor);
                 var id = r.Select(c => c.CourseId).SingleOrDefault();
                

                if (id != null)
                    {
                       
                        studentsIQ = studentsIQ.Where(x => x.CourseId == id);
                        Student = await studentsIQ.ToListAsync();

                    }
                    else
                    {
                        a = "Selected Insturctor Has Not  assign this Course";

                        INS = new SelectList(await INSQuery.Distinct().ToListAsync());
                        Cou = new SelectList(await COUQuery.Distinct().ToListAsync());
                    Student = await studentsIQ.ToListAsync();
                    return Page();

                    }     
            }
            else
            {
                a = "Invalid Selection";
            }
            Student = await studentsIQ.ToListAsync();
            return Page();
        }
    }
}
