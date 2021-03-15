using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLover.Models.UserProfileModels
{
    public class UserProfileBookReviewDisplay
    {
        public int ReviewId { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public double BookRating { get; set; }
        public string ReviewText { get; set; }
    }
}
