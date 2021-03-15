using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLover.Data
{
    public class Author
    {

        [Key]
        public int AuthorId { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Description { get; set; }

        public virtual List<Book> BookList { get; set; }
        public virtual List<BookReview> BookReviews { get; set; }

        public double AverageRating
        {
            get
            {
                if (BookReviews.Count > 0)
                {
                    return BookReviews.Select(r => r.BookRating).Sum() / BookReviews.Count;
                }
                return 0;
            }
        }
    }
}

