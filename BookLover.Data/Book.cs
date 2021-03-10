﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLover.Data
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Genre { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
        public double AverageRating 
        {
            get
            {
                return Reviews.Select(r => r.BookRating).Sum() / Reviews.Count;
            }
        }

        public virtual List<BookReview> Reviews { get; set; }
        public virtual List<Bookshelf> Bookshelves { get; set; } 

        /*        [ForeignKey(nameof(Author))]
                public int AuthorId { get; set; }
                public virtual Author Author { get; set; }*/
    }
}
