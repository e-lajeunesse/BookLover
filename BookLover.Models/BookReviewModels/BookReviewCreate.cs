using System;
using System.ComponentModel.DataAnnotations;

namespace BookLover.Models
{
    public class BookReviewCreate
    {
        public string ReviewTitle { get; set; }
        public string ReviewText { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Score must be between 1 and 10")]
        public double BookRating { get; set; }
        public int BookId { get; set; }
    }
}
