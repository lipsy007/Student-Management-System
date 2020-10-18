using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ishant_Goyal.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Ishant_Goyal.Pages.Students
{
  
    public class IndexModel : PageModel
    {
        private readonly Ishant_Goyal.Models.IshantContext _context;

        public IndexModel(Ishant_Goyal.Models.IshantContext context)
        {
            _context = context;
        }

        public IList<Student> Student { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        [BindProperty(SupportsGet = true)]
        public string stateString { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SelectOperator { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SelectOperator1 { get; set; }
        [BindProperty(SupportsGet = true)]
        public string CourseString { get; set; }
        public string NameSort { get; set; }
        public string LSort { get; set; }
        public string fSort { get; set; }
        public string Dobsort { get; set; }
        public SelectList Cou { get; set; }
        public SelectList st { get; set; }
        public SelectList Citylist { get; set; }
        public string CurrentSort { get; private set; }
        public string searchcou { get; set; }
        public PaginatedList<Student> Students { get; set; }
        public string searchdSta { get; set; }
        public string CurrentFilter { get; set; }
        public string Msg { get; set; }
        public string searchcity { get; private set; }

        public async Task OnGetAsync(string SearchString, string sortOrder, string searchcou,
            string searchdSta, string currentFilter, int? pageIndex)

        {
            IQueryable<string> DeptQuery = from m in _context.Course
                                           orderby m.CourseName
                                           select m.CourseName;
            IQueryable<string> CQuery = from m in _context.State
                                        orderby m.StateName
                                        select m.StateName;
            
            IQueryable<Student> studentsIQ = from s in _context.Student.Include(c => c.Course).Include(d => d.StateCodeNavigation).Include(e => e.CityCodeNavigation)
                                             select s;

            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            LSort = sortOrder == "Date" ? "Date_desc" : "Date";
            fSort = sortOrder == "Fee" ? "Fee_desc" : "Fee";
            Dobsort = sortOrder == "Dob" ? "Dob_desc" : "Dob";

            st = new SelectList(await CQuery.Distinct().ToListAsync());
            Cou = new SelectList(await DeptQuery.Distinct().ToListAsync());
            
            if (SearchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                SearchString = currentFilter;
            }



            switch (sortOrder)
            {
                case "name_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.FirstName);
                    break;
                case "Date_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.Course.CourseName);
                    break;
                case "Date":
                    studentsIQ = studentsIQ.OrderBy(s => s.Course.CourseName);
                    break;
                case "Dob_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.Dob);
                    break;
                case "Dob":
                    studentsIQ = studentsIQ.OrderBy(s => s.Dob);
                    break;
                case "Fee_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.Fees);
                    break;
                case "Fee":
                    studentsIQ = studentsIQ.OrderBy(s => s.Fees);
                    break;

                default:
                    studentsIQ = studentsIQ.OrderBy(s => s.FirstName);
                    break;
            }
            int pageSize = 3;
            Students = await PaginatedList<Student>.CreateAsync(
                studentsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
                
        }

        public async Task<IActionResult> OnPostAsync(string SearchString, string sortOrder, string searchcou,
            string searchdSta, int? pageIndex)
        {


            IQueryable<string> DeptQuery = from m in _context.Course
                                           orderby m.CourseName
                                           select m.CourseName;
            IQueryable<string> CQuery = from m in _context.State
                                        orderby m.StateName
                                        select m.StateName;

            st = new SelectList(await CQuery.Distinct().ToListAsync());
            Cou = new SelectList(await DeptQuery.Distinct().ToListAsync());

            IQueryable<Student> studentsIQ = from s in _context.Student.Include(c => c.Course).Include(d => d.StateCodeNavigation).Include(e => e.CityCodeNavigation)
                                             select s;

            if (string.IsNullOrEmpty(searchcou) && string.IsNullOrEmpty(searchdSta) && string.IsNullOrEmpty(SearchString))
            {
                Msg = "One field Must be  Required";
            }
            else
            {
                if ( SelectOperator == "AND" && SelectOperator1 == "AND")
                {
                    studentsIQ = studentsIQ.Where(s => s.FirstName.Contains(SearchString) && s.Course.CourseName == searchcou && s.StateCodeNavigation.StateName == searchdSta);
                }
                else if (SelectOperator == "OR" || SelectOperator1 == "OR" || SelectOperator == "OR" && SelectOperator1 == "OR")
                {
                    studentsIQ = studentsIQ.Where(s => s.FirstName.Contains(SearchString) || s.Course.CourseName == searchcou || s.StateCodeNavigation.StateName == searchdSta);
                }
                else if (SelectOperator == "AND" && SelectOperator1 == "OR")
                {
                    studentsIQ = studentsIQ.Where(s => s.FirstName.Contains(SearchString) && s.Course.CourseName == searchcou || s.StateCodeNavigation.StateName == searchdSta);

                }
                else if (SelectOperator == "OR" && (SelectOperator1 == "AND"))
                {
                    studentsIQ = studentsIQ.Where(s => s.FirstName.Contains(SearchString) || s.Course.CourseName == searchcou && s.StateCodeNavigation.StateName == searchdSta);
                }
                else if (SelectOperator1 == "AND" && string.IsNullOrEmpty(searchcou))
               {
                    studentsIQ = studentsIQ.Where(s => s.FirstName.Contains(SearchString) && s.StateCodeNavigation.StateName == searchdSta);
               }
                else if (SelectOperator=="AND" || SelectOperator1 == "AND" && string.IsNullOrEmpty(SearchString))
                {
                    studentsIQ = studentsIQ.Where(s => s.Course.CourseName == searchcou && s.StateCodeNavigation.StateName == searchdSta);
                }
                else
                {
                    studentsIQ = studentsIQ.Where(s => s.FirstName.Contains(SearchString) && s.Course.CourseName == searchcou);
                }
            }
           


            //if (!string.IsNullOrEmpty(searchdSta))
            //{
            //    studentsIQ = studentsIQ.Where(x => );
            //}

           

            int pageSize = 3;
            Students = await PaginatedList<Student>.CreateAsync(
                studentsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
            return Page();
        }
    }
    
}
