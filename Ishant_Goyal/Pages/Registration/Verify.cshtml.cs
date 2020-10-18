using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ishant_Goyal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Ishant_Goyal.Pages.Registration
{
    //[Authorize(Roles = "Admin,User")]
    public class VerifyModel : PageModel
    {
        private readonly Ishant_Goyal.Models.IshantContext _context;

        public VerifyModel(Ishant_Goyal.Models.IshantContext context)
        {
            _context = context;
        }
        [BindProperty]
        public User User { get; set; }
        public String Msg { get; set; }
        public async Task<IActionResult> OnGetAsync(string id)
         {

            User  = _context.User.Where(u => u.ActivationCode == id).FirstOrDefault();
            if (User != null)
            {
                User.ConfirmEmail = true;

            }
            _context.Attach(User).State = EntityState.Modified;

            
                await _context.SaveChangesAsync();
            Msg = "Your Account Is Activated Please Login ";
            return Page();




         }       

    }
}
 