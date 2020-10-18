using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ishant_Goyal.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool ConfirmEmail { get; set; }
        [NotMapped]
        public string ConfirmPassword { get; set; }
        public bool? Loggeduser { get; set; }
        public string ActivationCode { get; set; }
    }
}
