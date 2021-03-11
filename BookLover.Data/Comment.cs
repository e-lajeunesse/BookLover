using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLover.Data
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        public Guid OwnerId { get; set; }

        public DateTimeOffset CreatedUtc { get; set; }

        [Required]
        [MaxLength(500)]
        public string CommentText { get; set; }

        [ForeignKey(nameof(BookReview))]
        public int ReviewId { get; set; }
        public virtual BookReview BookReview { get; set; }


    }
}
