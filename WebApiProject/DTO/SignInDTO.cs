using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiProject.DTO
{
    /// <summary>
    /// Sign In Data Trasfer Object
    /// </summary>
    public class SignInDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
