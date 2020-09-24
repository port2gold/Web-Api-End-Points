using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiProject.Model;

namespace WebApiProject.Services
{
    public class WebAppDbContext :IdentityDbContext<AppUser>
    {
        public WebAppDbContext(DbContextOptions<WebAppDbContext> options) : base(options) { }
        

        
    }
}
