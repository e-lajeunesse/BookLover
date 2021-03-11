using BookLover.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLover.Models.UserProfileModels
{
    public class UserProfileCreate
    {
        public int UserProfileId { get; set; }
        public string UserName { get; set; }
        [Display(Name="UserCreated")]
        public DateTimeOffset CreatedUser { get; set; }
        // public List<Book> BookToRead { get; set; }
        // public List<int> BookIds { get; set; }
    }
}
