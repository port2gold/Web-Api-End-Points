using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiProject.Model
{
    public class UserModel
    {
        /// <summary>
        /// User Model with different fields
        /// </summary>
        [Required]
        [MaxLength (25, ErrorMessage ="Max First Name Characters is 25")]
        public string FirstName { get; set; }
        [MaxLength (25, ErrorMessage ="Max Last Name Characters is 25")]
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}
