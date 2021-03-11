using BookLover.Models.BookReviewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLover.Models.BookModels
{
    public class BookListItem
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public double AverageRating { get; set; }
        public List<BookReviewDisplayItem> BookReviews { get; set; }


        //Ben's changes
        public int AuthorId { get; set; }
        public AuthorListItems Author { get; set; }
        
    }
}
