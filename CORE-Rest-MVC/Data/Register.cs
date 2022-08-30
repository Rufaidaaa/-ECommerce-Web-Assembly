using CORE_Rest_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_Rest_MVC.Data
{
    public class Register
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public string Email { get; set; }

        public string Pwd { get; set; }
        public string Confirmpwd { get; set; }
        public List<User> User { get; set; }

    }
}
