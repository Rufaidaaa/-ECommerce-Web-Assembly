using CORE_Rest_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_rest_api.Models
{
    public interface ISignIn
    {
        Task<User> Login(LoginUser login);
    }
}
