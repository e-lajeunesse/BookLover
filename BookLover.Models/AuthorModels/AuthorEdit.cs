using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLover.Models
{
    public class AuthorEdit
    {
        public int AuthorId { get; set; }
        
        [Required]
        [MinLength(2, ErrorMessage = "Please enter at least 2 characters")]
        public string FirstName { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Please enter at least 2 characters")]
        public string LastName { get; set; }
        public string Description { get; set; }
    }
}
