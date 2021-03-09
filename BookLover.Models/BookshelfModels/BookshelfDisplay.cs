﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLover.Models.BookshelfModels
{
    public class BookshelfDisplay
    {
        public int BookshelfId { get; set; }
        public string Title { get; set; }
        public List<BookshelfBookDisplay> Books { get; set; }
    }
}
