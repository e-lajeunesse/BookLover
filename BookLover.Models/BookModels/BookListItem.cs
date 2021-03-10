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
    }
}
