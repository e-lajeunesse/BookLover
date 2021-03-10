using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLover.Data
{
    public class BookReview
    {
        [Key]
        public int ReviewId { get; set; }
        public Guid OwnerId { get; set; }
        public string ReviewTitle { get; set; }
        [Required]
        public string ReviewText { get; set; }
        public DateTimeOffset CreatedReview { get; set; }
        public double BookRating { get; set; }

        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}
