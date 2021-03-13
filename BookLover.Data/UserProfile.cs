using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLover.Data
{
    public class UserProfile
    {
        [Key]
        public int UserProfileId { get; set; }
        public Guid OwnerId { get; set; }
        public string UserName { get; set; }
        public virtual List<Bookshelf> Bookshelves { get; set; }
        public virtual List<BookReview> BookReviews { get; set; }

        [ForeignKey(nameof(Book))]
        public List<int> BookIds { get; set; }
        public virtual List<Book> BooksToRead { get; set; }
    }
}
