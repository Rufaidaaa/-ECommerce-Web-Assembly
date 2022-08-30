using BlazorProject.Server.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_Rest_MVC.Models
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly AppDbContext appDbContext;
        private IHostingEnvironment env;
       
        public ProfileRepository(AppDbContext appDbContext, IHostingEnvironment hostingEnv)
        {
            this.appDbContext = appDbContext;
            env = hostingEnv;
        }
        public async Task<Profile> GetProfile(int ProfileId)
        {
            return await appDbContext.Profile.FirstOrDefaultAsync(e => e.ProfileId == ProfileId);
        }


        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(env.ContentRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return "/" + folderPath;
        }

        public async Task<Profile> UpdateProfile(Profile profile)
        {
            var result = await appDbContext.Profile
               .FirstOrDefaultAsync(e => e.ProfileId == profile.ProfileId );

            string wwwRootPath = env.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(profile.Image.FileName);
            string extension = Path.GetExtension(profile.Image.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "/Images/", fileName);
            profile.PhotoPath = Path.Combine( "/Images/", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await profile.Image.CopyToAsync(fileStream);
            }

            if (result != null)
            {
                result.bio = profile.bio;
                result.username = profile.username;
                result.email = profile.email;
                result.contact = profile.contact;
                result.PhotoPath = profile.PhotoPath;

                await appDbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
