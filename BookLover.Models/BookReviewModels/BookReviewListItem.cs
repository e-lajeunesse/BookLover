﻿using BookLover.Models.BookModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLover.Models.BookReviewModels
{
    public class BookReviewListItem
    {
        public int ReviewId { get; set; }
        public int BookId { get; set; }
        public double BookRating { get; set; }
        public string ReviewTitle { get; set; }
        public string ReviewText { get; set; }

        [Display(Name = "Created")]
        public DateTimeOffset CreatedReview { get; set; }
        public BookDisplayItem Book { get; set; }
    }
}
