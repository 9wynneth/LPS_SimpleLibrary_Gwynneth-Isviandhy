using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPS_SimpleLibrary.Model
{
    public class BookModel
    {
        //Fields
        private string id;
        private string name;
        private string genre;
        private string author;
        private string status;

        //Properties - Validations
        [DisplayName("Book ID")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [DisplayName("Book Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Book name must consist between 5-50 characters")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        [DisplayName("Genre")]
        [Required(ErrorMessage = "Genre of the book is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Genre must consist between 3-50 characters")]
        public string Genre
        {
            get { return genre; }
            set { genre = value; }
        }

        [DisplayName("Author")]
        [Required(ErrorMessage = "Author of the book is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Author must consist between 3-50 characters")]
        public string Author
        {
            get { return author; }
            set { author = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}
