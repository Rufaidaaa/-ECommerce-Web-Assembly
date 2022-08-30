using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_Rest_MVC.Models
{
    public class Image
    {
        
        public string ImagePath { get; set; }

     
        public List<IFormFile> ImageFile { get; set; }
    }
}
