using BlazorProject.Server.Models;
using Employee_rest_api.Models;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webgentle.BookStore.Data;
using Webgentle.BookStore.Models;

namespace CORE_Rest_MVC.Models
{
    public class ImageRepository : IImageRepository
    {

        private IHostEnvironment _env;
        private readonly AppDbContext _appDbContext;
        public ImageRepository(IHostEnvironment env, AppDbContext appDbContext)
        {
            _env = env;
            _appDbContext = appDbContext;
        }

        public async Task<Books> AddNewImage(BookModel model)
        {
            var newBook = new Books()
            {
                //Author = model.Author,
                CreatedOn = DateTime.UtcNow,
                //Description = model.Description,
                Title = model.Title,
                //LanguageId = model.LanguageId,
                //TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,
                UpdatedOn = DateTime.UtcNow,
                //CoverImageUrl = model.CoverImageUrl,
                //BookPdfUrl = model.BookPdfUrl
            };

            newBook.bookGallery = new List<BookGallery>();

            foreach (var file in model.Gallery)
            {
                newBook.bookGallery.Add(new BookGallery()
                {
                    Name = file.Name,
                    URL = file.URL
                });
            }

            await _appDbContext.Books.AddAsync(newBook);
            await _appDbContext.SaveChangesAsync();

            return newBook;
        }
    }
}
