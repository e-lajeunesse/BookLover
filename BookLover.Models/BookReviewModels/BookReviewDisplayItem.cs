﻿using BookLover.Models.BookModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLover.Models.BookReviewModels
{
    public class BookReviewDisplayItem
    {
        public int ReviewId { get; set; }
        public double BookRating { get; set; }
        public string ReviewText { get; set; }
        // public List<BookDisplayItem> Books { get; set; }
    }
}
