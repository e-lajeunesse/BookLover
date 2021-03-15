using BookLover.Data;
using BookLover.Models.BookshelfModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLover.Models.UserProfileModels
{
    public class UserProfileListItem
    {
        public int UserProfileId { get; set; }
        public string UserName { get; set; }
        public List<BookToReadDisplay> BooksToRead { get; set; }
        
        public List<BookshelfDisplay> Bookshelves { get; set; }

        public List<UserProfileBookReviewDisplay> BookReviews { get; set; }
        

       
    }
}
