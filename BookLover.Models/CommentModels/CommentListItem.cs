using BookLover.Models.BookReviewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLover.Models.CommentModels
{
    public class CommentListItem
    {
        public int CommentId { get; set; }
        public string CommentText { get; set; }
        [Display(Name = "Posted on ")]
        public DateTimeOffset CreatedUtc { get; set; }
        public int ReviewId { get; set; }
        public BookReviewDisplayItem Review { get; set; }
    }
}
