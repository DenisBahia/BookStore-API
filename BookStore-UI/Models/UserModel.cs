using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore_UI.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }

    public class RegistrationModel
    {
        [Required]
        [EmailAddress]
        [Display(Name ="Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Password is limited to {2} to {1} chars.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Password is limited to {2} to {1} chars.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="The password and confirmation must match.")]
        public string ConfirmPassword { get; set; }
    }

}
