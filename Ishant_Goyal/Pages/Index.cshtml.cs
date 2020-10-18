using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ishant_Goyal.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using static Ishant_Goyal.Utility;
using Ishant_Goyal.services;

namespace Ishant_Goyal.Pages
{
    public class IndexModel : PageModel
    {
          private readonly Encryption _encrypt;
        private IUserservices _Userservices;


        public IndexModel(IUserservices Userservices, Encryption encrypt)
        {
            _encrypt = encrypt;
            _Userservices = Userservices;
        }

        public IActionResult OnGet()
        {

            return Page();
        }

        [BindProperty]
        public User User { get; set; }
        public User data { get; set; }
        public string Msg { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                data = _Userservices.Getuserbyemail(User.Email);

                if (data != null)
                {
                    var keys = _encrypt.key;
                    data.Password = Utility.DecryptString(data.Password, keys);
                    if (data.Password == User.Password)
                    {
                        if (data.ConfirmEmail == true)
                        {
                            data.Loggeduser = true;
                            var claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.Email, User.Email),
                            new Claim(ClaimTypes.Role,"Admin")


                        };
                            var userIdentity = new ClaimsIdentity(claims, "User Identity");
                            var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });
                            await HttpContext.SignInAsync(userPrincipal);
                            return RedirectToPage("/Registration/Welcome");

                        }

                        else
                        {
                            Msg = "Activate your account";
                            return Page();
                        }
                    }
                    else
                    {
                        Msg = "Incorrect Password";
                        return Page();
                    }
                }
                else
                {
                    Msg = "User not Exists";
                    return Page();
                }
            }

            catch (Exception ex)
            {
                return Page();
            }

        }

    }
}
