using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webgentle.BookStore.Data;
using Webgentle.BookStore.Models;

namespace CORE_Rest_MVC.Models
{
    public interface IImageRepository
    {
        Task<Books> AddNewImage(BookModel model);
    }
}
