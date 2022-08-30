using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webgentle.BookStore.Data
{
    public class Books
    {
        public int Id { get; set; }
        public string Title { get; set; }
        
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

       // public Language Language { get; set; }

        public ICollection<BookGallery> bookGallery { get; set; }
    }
}
