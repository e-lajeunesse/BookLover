using BookLover.Data;
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
    }
}
