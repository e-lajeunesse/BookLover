using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLover.Models.BookshelfModels
{
    public class BookshelfCreate
    {
        public string Title { get; set; }
        public List<int> BookIds { get; set; }
    }
}
