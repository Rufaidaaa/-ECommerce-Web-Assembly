using BlazorProject.Server.Models;
using CORE_Rest_MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CORE_Rest_MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : Controller
    {
        private readonly AppDbContext appDbContext;
        private readonly IRegisterRepository registerRepository;

        public RegisterController(AppDbContext appDbContext, IRegisterRepository registerRepository)
        {
            
            this.appDbContext = appDbContext;
            this.registerRepository = registerRepository;
        }


        [HttpPost]

        public async Task<ActionResult<User>> Index(User user)

        {
            if (user == null)
            {
                return BadRequest();
            }

            if (appDbContext.Users.Any(x => x.Email == user.Email))
            {
                return Conflict("User already exist");
            }

            string EncryptionKey = "MAKVKKKBNI99212";
            var res = "";
            byte[] clearBytes = Encoding.Unicode.GetBytes(user.Pwd);
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
                user.Pwd = res;
                var result = await registerRepository.RegisterUser(user);
                return user;
            }

        }

    }
}
