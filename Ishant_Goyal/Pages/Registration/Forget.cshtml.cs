using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Ishant_Goyal.Models;
using Ishant_Goyal.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;

namespace Ishant_Goyal.Pages.Registration
{
    
    public class ForgetModel : PageModel
    {
        private readonly Ishant_Goyal.Models.IshantContext _context;
        private IUserservices _Userservices;
        private readonly IEmailServices _emailServices;
        public ForgetModel(Ishant_Goyal.Models.IshantContext context,IUserservices Userservices, IEmailServices emailServices)
        {
            _context = context;
            _Userservices = Userservices;
            _emailServices = emailServices;
        }
        [BindProperty]
        public  User User { get; set; }
         public string Msg { get; set; }

        public User data { get; set; }
        public void OnGet()
        {


        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
             data =_Userservices.Getuserbyemail(User.Email);
           if (data != null)
            {
                if (data.ConfirmEmail == true)
                {
                    string emailUrl = "http://localhost:53951/Registration/ResetPass" + "?id=" + data.ActivationCode;

                    _emailServices.SendEmail(User.Email, CreateEmailMessage(emailUrl), "Registration");
                    return RedirectToPage("./Login");
                }
                else
                    Msg = "You need to Activate Your Account";
            }
            else
            {
                Msg = "Email id  is not exist ";
            }


            return Page();
            
        }
        private string CreateEmailMessage(string url)
        {
            string body = "<br/><br/>For reset your password Click below" +
      " <br/><br/>" +
      "<a href='" + url + "'><button > <h3>Reset Password</h3></button></a>";

            return body;
        }
    }
}
