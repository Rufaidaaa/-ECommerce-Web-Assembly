using BlazorProject.Server.Models;
using CORE_Rest_MVC.Models;
using Employee_rest_api;
using Employee_rest_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JwtApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController : ControllerBase
    {
        //private IConfiguration _config;
        private static string _config = "DhftOS5uphK3vmCJQrexST1RsyjZBjXWRgJMFPU4";
        private static string Issuer = "https://localhost:44301/";
        private static string Audience = "https://localhost:44301/";
        private AppDbContext _AppDbContext;
        [ActivatorUtilitiesConstructor]
        public SignInController(AppDbContext AppDbContext)
        {
            _AppDbContext = AppDbContext;
        }

        //public SignInController(IConfiguration config)
        //{
        //    _config = config;
        //}

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginUser userLogin)
        {
            var user = Authenticate(userLogin);

            if (user != null)
            {
                var token = Generate(user);
                var response = new
                {
                    User = userLogin,
                    Token = token
                };
                return Ok(response);
            }
          

            return NotFound("Email or Password is incorrect!");
        }

        private string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
            };

            var token = new JwtSecurityToken(Issuer,
              Audience,
              claims,
              expires: DateTime.Now.AddMinutes(55),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User Authenticate(LoginUser login)
        {
            string EncryptionKey = "MAKVKKKBNI99212";
            var res = "";
            byte[] clearBytes = Encoding.Unicode.GetBytes(login.Password);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    res = Convert.ToBase64String(ms.ToArray());
                }
                login.Password = res;

            }
            var currentUser = _AppDbContext.Users.FirstOrDefault(e => e.Email == login.Email && e.Pwd == login.Password);

            if (currentUser != null)
            {
                return currentUser;
            }
            

            return null;
        }
    }
}