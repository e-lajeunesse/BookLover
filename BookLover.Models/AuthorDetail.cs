﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLover.Models
{
    public class AuthorDetail
    {
        public int AuthorId { get; set; }
        public Guid UserId { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Please enter at least 2 characters")]
        public string FirstName { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Please enter at least 2 characters")]
        public string LastName { get; set; }
        public List<string> Books { get; set; }

        public string Description { get; set; }

       
    }
}
