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
        public Guid UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public List<string> Books { get; set; }

        public string Description { get; set; }


        //public virtual List<BookReview> Reviews { get; set; }
    }
}
