using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiProject.DTO
{
    public class NewuserDTO
    {
        /// <summary>
        /// New User Data Trasfer Object
        /// </summary>
        /// //username
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
