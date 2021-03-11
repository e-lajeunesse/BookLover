using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLover.Models.CommentModels
{
    public class CommentCreate
    {
        public string CommentText { get; set; }
        public int ReviewId { get; set; }
    }
}
