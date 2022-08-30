using BlazorProject.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CORE_Rest_MVC.Models
{
    public class RegisterRepository : IRegisterRepository
    {
        private readonly AppDbContext appDbContext;

        public RegisterRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<User> RegisterUser(User user)
        {

            if (user != null)
            {
                user.Status = "Hey there!";
            }
            Profile profile = new Profile();
            profile.username = user.Username;
            profile.email = user.Email;
           
            var result = await appDbContext.Users.AddAsync(user);
            var result2 = await appDbContext.Profile.AddAsync(profile);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

       

    }
}
