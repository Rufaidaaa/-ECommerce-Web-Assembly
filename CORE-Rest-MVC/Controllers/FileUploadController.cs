using BlazorProject.Server.Models;
using Employee_rest_api.Models;
//using Employee_rest_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Microsoft.EntityFrameworkCore;
using Webgentle.BookStore.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using CORE_Rest_MVC.Models;

namespace uploadimage.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class FileUploadController : Controller
    {
      
        private IHostEnvironment _env;
        private readonly AppDbContext _appDbContext;
        private readonly IImageRepository _ImageRepository = null;
        public FileUploadController(IHostEnvironment env, AppDbContext appDbContext, IImageRepository imageRepository)
        {
            _env = env;
            _appDbContext = appDbContext;
            _ImageRepository = imageRepository;
        }
        


        //public FileUploadController(AppDbContext appDbContext)
        //{
        //    this.appDbContext = appDbContext;
        //}
       
        public string Base64Converter(string path)
        {
            string base64Image = Convert.ToBase64String(Encoding.ASCII.GetBytes(path));
            return base64Image;
        }
        [HttpPost]
        public async Task<BookModel> Add([FromForm] BookModel bookModel)
        {
           // var identity = HttpContext.User.Identity as ClaimsIdentity;

            try
            {
                //if (identity != null)
                //{
                //    IEnumerable<Claim> claims = identity.Claims;
                //    // or
                //    var iden = identity.FindFirst(ClaimTypes.NameIdentifier).Value;

                    if (bookModel.GalleryFiles != null)
                    {
                        string folder = "books/";

                        bookModel.Gallery = new List<GalleryModel>();

                        foreach (var file in bookModel.GalleryFiles)
                        {
                            var gallery = new GalleryModel()
                            {
                                Name = file.FileName,
                                URL = await UploadImage(folder, file)
                            };
                            bookModel.Gallery.Add(gallery);
                        }
                    }
                    await _ImageRepository.AddNewImage(bookModel);
                    //if (id > 0)
                    //{
                    //    return RedirectToAction(nameof(Add), new { isSuccess = true, bookId = id });
                    //}
                    //foreach (var file in image.ImageFile)
                    //{
                    //    if (file != null && file.Length > 0)
                    //    {
                    //        image.Name = Path.GetFileName(file.FileName);
                    //        //string extension = Path.GetExtension(file.FileName);
                    //       // Filename = Filename + DateTime.Now.ToString(" yyyyhhmmssfff ") + extension;
                    //        image.ImagePath =  image.Name;
                    //        Filename = Path.Combine(_env.ContentRootPath, "wwwroot", "Images", Filename);
                    //        using (Stream fileStream = new FileStream(Filename, FileMode.Create, FileAccess.Write))
                    //        {
                    //            file.CopyTo(fileStream);
                    //        }


                    //        //using (var transaction = _appDbContext.Database.BeginTransaction())
                    //        //{
                    //        //    _appDbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Image OFF;");
                    //            _appDbContext.Image.Add(image.ImagePath);
                    //          //  _appDbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Image ON");

                    //            _appDbContext.SaveChanges();
                    //           // _appDbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Image OFF;");
                    //        //    transaction.Commit();
                    //        //}
                    //    }

                    //}

                    //foreach (var file in image.ImageFile)
                    //{
                    //  if (file != null && file.Length > 0)
                    //  {
                    //    string Filename = Path.GetFileNameWithoutExtension(file.FileName);
                    //    string extension = Path.GetExtension(file.FileName);
                    //    Filename = Filename + DateTime.Now.ToString(" yyyyhhmmssfff ") + extension;
                    //    image.ImagePath = "~/image/" + Filename;
                    //    Filename = Path.Combine(_env.ContentRootPath, Filename);
                    //        using (Stream fileStream = new FileStream(Filename, FileMode.Create, FileAccess.Write))
                    //        {
                    //            file.CopyTo(fileStream);
                    //        }
                    //  }
                    //    //using (var transaction = _appDbContext.Database.BeginTransaction())
                    //    //{
                    //        _appDbContext.Image.Add(image);
                    //        // _appDbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Image ON");
                    //        //_appDbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Image ON;");
                    //        _appDbContext.SaveChanges();
                    //    //    _appDbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Image OFF;");
                    //    //    transaction.Commit();
                    //    //}

                    //}



                    // _appDbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Image OFF");

                //}
            }
            catch (Exception ex)
            {
                //foreach (var errors in ex.EntityValidationErrors)
                //{
                //    foreach (var validationError in errors.ValidationErrors)
                //    {
                //        // get the error message 
                //        string errorMessage = validationError.ErrorMessage;
                //    }
                //}
            }
            //string Filename = Path.GetFileNameWithoutExtension(image.ImageFile.FileName);
            //string extension = Path.GetExtension(image.ImageFile.FileName);
            //Filename = Filename + DateTime.Now.ToString(" yyyyhhmmssfff ") + extension;
            //image.ImagePath = "~/image/" + Filename;
            //Filename = Path.Combine(Server.MapPath("~/image/"), Filename);
            //image.ImageFile.SaveAs(Filename);

            ModelState.Clear();
            return bookModel;
        }

        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_env.ContentRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return "/" + folderPath;
        }
       

       
        
    }
}