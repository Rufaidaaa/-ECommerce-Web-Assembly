using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorProject.Shared
{
    public class Employee
    {
        public int EmployeeId { get; set; }
      //  [Required(ErrorMessage = "The FirstName is required")]
        [MinLength(3, ErrorMessage = "FirstName must contains at least 3 charcters")]
        public string FirstName { get; set; }
       // [Required(ErrorMessage = "The LastName is required")]
        public string LastName { get; set; }
       // [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public DateTime DateOfBrith { get; set; }
      //  [Required(ErrorMessage = "The gender is required")]
        public Gender Gender { get; set; }
        public int DepartmentId { get; set; }
        public string PhotoPath { get; set; }

        [NotMapped]
        public List<IFormFile> ImageFile { get; set; }
        public Department Department { get; set; }
    }
}
