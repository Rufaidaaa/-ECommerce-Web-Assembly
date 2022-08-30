using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;

namespace Webgentle.BookStore.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        //[StringLength(100, MinimumLength = 5)]
        //[Required(ErrorMessage ="Please enter the title of your book")]
        public string Title { get; set; }
       // [Required(ErrorMessage ="Please enter the author name")]
        public string Author { get; set; }
        

        //[Display(Name = "Choose the gallery images of your book")]
        //[Required]
        public IFormFileCollection GalleryFiles { get; set; }

        public List<GalleryModel> Gallery { get; set; }

       
    }
}
