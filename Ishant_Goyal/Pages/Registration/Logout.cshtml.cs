using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ishant_Goyal.Pages.Registration
{
    //[Authorize(Roles = "Admin,User")]
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {

        }

      public async Task<IActionResult> OnPostAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToPage("./Login");
        }
    }
}
