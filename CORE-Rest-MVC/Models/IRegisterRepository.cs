using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_Rest_MVC.Models
{
    public interface IRegisterRepository
    {
        Task<User> RegisterUser(User employee);
    }
}
