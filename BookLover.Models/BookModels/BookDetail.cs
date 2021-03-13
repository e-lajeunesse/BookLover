using BookLover.Data;
using BookLover.Models.BookReviewModels;
using BookLover.Models.BookshelfModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLover.Models.BookModels
{
    public class BookDetail
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public double AverageRating { get; set; }

        [Display(Name = "Ratings")]
        public int ReviewCount { get; set; }
        
        public int AuthorId { get; set; }
        public AuthorDisplayItem Author { get; set; }
        public BookshelfDisplay RecommendedBooks { get; set; }       
        public List<BookReviewDisplayItem> BookReviews { get; set; }
    }
}
