using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ishant_Goyal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;
using Ishant_Goyal.services;
using System.IO;

namespace Ishant_Goyal.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly IshantContext _context;

       
        private IUserservices _Userservices;
        public CreateModel( IUserservices Userservices, IshantContext context)
        {
            _Userservices = Userservices;
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            CityInfo = await _context.City.ToListAsync();

            CInfo = _Userservices.GetActiveCourse().ToList();
            sta = _Userservices.GetStates().ToList();
            //ViewData["StateCode"] = new SelectList(_Userservices.GetStates(), "StateCode", "StateName");
            //ViewData["CityCode"] = new SelectList(_Userservices.GetCitys(), "CityCode", "CityName");
            return Page();
        }
        [BindProperty]
        public List<City> CityInfo { get; private set; }
        [BindProperty]
        public Student Student { get; set; }
        public string Msg { get; private set; }
        [BindProperty]
        public List<Course> CInfo { get; set; }
        [BindProperty]
        public List<State> sta{ get; private set; }


        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                CityInfo = _Userservices.GetCitys().ToList();
                sta = _Userservices.GetStates().ToList();
                CInfo = _Userservices.GetActiveCourse().ToList();
                return Page();
            }
            var CourseExist = _Userservices.GetStudents().Where(c => c.FirstName.Equals(Student.FirstName)).FirstOrDefault();
            if (CourseExist != null)
            {
                Msg = "The Name Exist already ";
                //CInfo = _Userservices.GetActiveCourse().ToList();
                CInfo = _Userservices.GetActiveCourse().ToList();
                //ViewData["CityCode"] = new SelectList(_Userservices.GetCitys(), "CityCode", "CityName");
                //ViewData["StateCode"] = new SelectList(_Userservices.GetStates(), "StateCode", "StateName");
                return Page();
            }
            Utility obj1 = new Utility();
             Student.Tax = obj1.CalculateTax((decimal)Student.Fees);
            await _Userservices.CreateAsync(Student);

            return RedirectToPage("./Index");
        }
        public async Task<IActionResult> OnPostCitysAsync()
        {
            MemoryStream stream = new MemoryStream();
            await Request.Body.CopyToAsync(stream);
            stream.Position = 0;
            using StreamReader reader = new StreamReader(stream);
            string requestBody = reader.ReadToEnd();
            if (requestBody.Length > 0)
            {

                CityInfo =  _Userservices.GetCity(requestBody).ToList();
                return new JsonResult(CityInfo);
            }
            return null;
        }

    }
}
