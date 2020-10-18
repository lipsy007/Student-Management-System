using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Ishant_Goyal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Ishant_Goyal.Pages.Registration
{
    //[Authorize(Roles = "Admin,User")]
    public class ResetPassModel : PageModel
    {
        private readonly Ishant_Goyal.Models.IshantContext _context;

        public ResetPassModel(Ishant_Goyal.Models.IshantContext context)
        {
            _context = context;
        }
        [BindProperty]
        public User User { get; set; }
        [BindProperty]
        public User User1 { get; set; }
        public String Msg { get; set; }
        public async Task<IActionResult> OnPostAsync(string id)
        {

            User1 = _context.User.Where(u => u.ActivationCode == id).FirstOrDefault();
            if (User1 != null)
            {
                User1.Password = User.Password;
                Msg = "Your Password Is reset ";
            }
            _context.Attach(User1).State = EntityState.Modified;


            await _context.SaveChangesAsync();

            
            return Page();




        }

 
    }

}
