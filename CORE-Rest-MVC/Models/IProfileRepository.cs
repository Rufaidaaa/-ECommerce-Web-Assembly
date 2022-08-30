using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_Rest_MVC.Models
{
    public interface IProfileRepository
    {
        Task<Profile> GetProfile(int id);
        Task<Profile> UpdateProfile(Profile Profile);
    }
}
