using ArelApp.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArelApp.UI.Identity
{
    public static class SeedIdentity
    {
        public static async Task Seed(UserManager<User> userManager,RoleManager<Role> roleManager,IConfiguration configuration)
        {
            var adminusername = configuration["Data:Admin:username"];
            var adminemail = configuration["Data:Admin:email"];
            var adminpassword = configuration["Data:Admin:password"];
            var adminrole = configuration["Data:Admin:role"];
            var academicianusername = configuration["Data:Academician:username"];
            var academicianemail = configuration["Data:Academician:email"];
            var academicianpassword = configuration["Data:Academician:password"];
            var academicianrole = configuration["Data:Academician:role"];
            var studentusername = configuration["Data:Student:username"];
            var studentemail = configuration["Data:Student:email"];
            var studentpassword = configuration["Data:Student:password"];
            var studentrole = configuration["Data:Student:role"];


            //admin
            if (await userManager.FindByNameAsync(adminusername) ==null)
            {

                await roleManager.CreateAsync(new Role(adminrole));
            }

            var admin = new User()
            {
                UserName=adminusername,
                Email=adminemail,
                Name="Admin",
                Surname="User",
            };

            var adminresult = await userManager.CreateAsync(admin, adminpassword);
            if (adminresult.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, adminrole);
            }

            // academician
            if (await userManager.FindByNameAsync(academicianusername) == null)
            {

                await roleManager.CreateAsync(new Role(academicianrole));
            }

            var academician = new User()
            {
                UserName = academicianusername,
                Email = academicianemail,
                Name = "Academician",
                Surname = "User",
            };

            var academicianresult = await userManager.CreateAsync(academician, academicianpassword);
            if (academicianresult.Succeeded)
            {
                await userManager.AddToRoleAsync(academician, academicianrole);
            }

            // student

            if (await userManager.FindByNameAsync(studentusername) == null)
            {

                await roleManager.CreateAsync(new Role(studentrole));
            }

            var student = new User()
            {
                UserName = studentusername,
                Email = studentemail,
                Name = "Student",
                Surname = "User",
            };

            var studentresult = await userManager.CreateAsync(student, studentpassword);
            if (adminresult.Succeeded)
            {
                await userManager.AddToRoleAsync(student, studentrole);
            }
        }
    }
}
