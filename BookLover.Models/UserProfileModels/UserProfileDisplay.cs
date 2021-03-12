using BookLover.Data;
using BookLover.Models.BookshelfModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLover.Models.UserProfileModels
{
    public class UserProfileDisplay
    {
        public int UserProfileId { get; set; }
        public string UserName { get; set; }
        public List<BookToReadDisplay> BooksToRead { get; set; }

        public List<BookshelfDisplay> Bookshelves { get; set; }
    }
}
