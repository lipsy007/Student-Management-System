using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ishant_Goyal.Models;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Ishant_Goyal.Utility;
using Ishant_Goyal.services;

namespace Ishant_Goyal.Pages.Registration
{
    public class SignupModel : PageModel
    {
        private readonly Ishant_Goyal.Models.IshantContext _context;
        private readonly  Encryption _encrypt;
        private readonly IEmailServices _emailServices;


        public SignupModel(Ishant_Goyal.Models.IshantContext context, Encryption encrypt, IEmailServices emailServices)
        {
            _encrypt = encrypt;
            _context = context;
            _emailServices = emailServices;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User User { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var keys = _encrypt.key;
            User.Password = Utility.EncryptString(User.Password,keys);
            User.ActivationCode = System.Guid.NewGuid().ToString();
            string emailUrl = "http://localhost:53951/Registration/verify" + "?id=" + User.ActivationCode; 
            _emailServices.SendEmail(User.Email, CreateEmailMessage(emailUrl), "Registration");
            _context.User.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Login");
        }
        private string CreateEmailMessage(string url)
        {
            string body = "<br/><br/>Verify Your Email To Activate your account " +
      " <br/><br/>" +
      "<a href='"+url+ "'><button style=background - color:orange; color: white> <h3>Click Here</h3></button></a>";

            return body;
        }
    }
}
