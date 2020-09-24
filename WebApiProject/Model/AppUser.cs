using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiProject.Model
{

    /// <summary>
    /// App User extending Identity User
    /// Adding fields to identity User
    /// </summary>
    public class AppUser: IdentityUser
    {
        [Required]
        [MaxLength(25, ErrorMessage = "Max First Name Characters is 25")]
        public string FirstName { get; set; }
        [MaxLength(25, ErrorMessage = "Max Last Name Characters is 25")]
        [Required]
        public string LastName { get; set; }
      
        
    }
}
