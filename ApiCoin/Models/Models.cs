using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApiCoin.Models
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class RegModel
    {
        public string Email { get; set; }

        [Required]
        [DataType("Password")]
        public string Password { get; set; }

        [DataType("Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match!")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}