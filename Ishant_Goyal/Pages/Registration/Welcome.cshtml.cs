using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ishant_Goyal.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Ishant_Goyal.Pages.Registration
{
    //[Authorize] 
    public class WelcomeModel : PageModel
    {
        
        private readonly Ishant_Goyal.Models.IshantContext _context;
        public WelcomeModel(Ishant_Goyal.Models.IshantContext context)
        {
            _context = context;
        }
        public User User { get; set;  }
        public Student Student { get; private set; }
           
        public void OnGet()
        {
            
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await HttpContext.SignOutAsync(
             CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("./Login");
        }
    }
}
