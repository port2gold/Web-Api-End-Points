using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiProject.Services;

namespace WebApiProject.Model
{
    /// <summary>
    ///Pre seeder to pre adds data into the database
    ///
    /// </summary>
    public class PreSeeder
    {
        public static async Task Seeder(WebAppDbContext ctx,RoleManager<IdentityRole>roleManager, UserManager<AppUser> userManager)
        {
            ctx.Database.EnsureCreated();
            if(!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
                
            }
            

            if(!userManager.Users.Any())
            {
                var listOfUsers = new List<AppUser>
                {
                    new  AppUser{UserName ="kaybee", Email="kaybeebaba@gmail.com", FirstName ="Kabir", LastName ="Omotoso"},
                    new AppUser{ UserName ="Shola", Email="sholagberuu@gmail.com",FirstName ="Sholagberu",LastName="Animashaun"},
                    new AppUser{UserName ="abdallah", Email="onaolapo@gmail.com",FirstName ="Abdullahi", LastName="Abdullahi"}
                };

                foreach(var user in listOfUsers)
                {
                    var result = await userManager.CreateAsync(user,"Ddontforgetme###01");
                    if(result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "User");
                    }
                }
            }
        }
    }
}
