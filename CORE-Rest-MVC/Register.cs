using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_rest_api
{
    public class Register
    {
        [Required(ErrorMessage = "The Username is required")]
        [MinLength(3, ErrorMessage = "Username must contains at least 3 charcters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "The email address is required")]
        public string Email { get; set; }

        [Display(Name ="Password")]
        [MinLength(8, ErrorMessage ="Password should be 8 characters long")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage ="The passwords do not match")]
        public string ConfirmPassword { get; set; }

    }
}
