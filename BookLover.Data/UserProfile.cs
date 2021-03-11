using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLover.Data
{
    public class UserProfile
    {
        [Key]
        public Guid OwnerId { get; set; }
        public List<string> BooksToRead { get; set; }
        public string UserName { get; set; }
    }
}
