using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLover.Models.BookModels
{
    public class BookCreate
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }

        public int AuthorId{get;set;}
    }
}
