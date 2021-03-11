using BookLover.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLover.Models.UserProfileModels
{
    public class UserProfileListItem
    {
        public int UserProfileId { get; set; }
        public string UserName { get; set; }
        public List<Book> BooksToRead { get; set; }
        [Display(Name="UserCreated")]
        public DateTimeOffset CreatedUser { get; set; }
    }
}
