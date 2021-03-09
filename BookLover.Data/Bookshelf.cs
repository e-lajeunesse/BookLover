using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLover.Data
{
    public class Bookshelf
    {
        [Key]
        public int BookshelfId { get; set; }

        [Required]
        public string Title { get; set; }

        [ForeignKey(nameof(Book))]
        [MaxLength(10)]
        public List<int> BookIds { get; set; }
        public virtual List<Book> Books { get; set; }
    }
}
