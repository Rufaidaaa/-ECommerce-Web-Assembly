using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_Rest_MVC.Models
{
    public class Profile
    {
        public int ProfileId { get; set; }
        public string username { get; set; }
        public string bio { get; set; }
        public string contact { get; set; }
        public string email { get; set; }

        public string PhotoPath { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
