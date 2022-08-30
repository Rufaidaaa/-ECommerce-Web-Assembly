using BlazorProject.Server.Models;
using CORE_Rest_MVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_rest_api.Models
{
    public class SignIn : ISignIn
    {
        private readonly AppDbContext appDbContext;

        public SignIn()
        {
        }

        public SignIn(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<User> Login(LoginUser login)
        {

            var result =  await appDbContext.Users.FirstOrDefaultAsync(e => e.Email == login.Email && e.Pwd == login.Password);
            return result;
        }
        
    }
}
