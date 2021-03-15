using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLover.Data
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Genre { get; set; }

        public static List<string> ValidGenres
        {
            get
            {
                return new List<string> { "Horror","Fantasy","Romance","Science Fiction","Thriller","Mystery",
                    "Young Adult","Historical Fiction","Nonfiction", "Comedy"};
            }
        }

        [MaxLength(500)]
        public string Description { get; set; }

        public int ReviewCount 
        { 
            get
            {
                return BookReviews.Count;
            }
        }
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
                       

        public virtual List<BookReview> BookReviews { get; set; }
        public virtual List<Bookshelf> Bookshelves { get; set; } 
        public virtual List<UserProfile> UsersWantingToRead { get; set; }

        [ForeignKey(nameof(Author))]
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
    }
}
