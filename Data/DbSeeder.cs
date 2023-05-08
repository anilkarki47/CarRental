using HajurKoCarRental.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace HajurKoCarRental.Data
{
    public class DbSeeder
    {
        public static async Task SeedUser(IApplicationBuilder applicationBuilder, RoleManager<IdentityRole> roleManager)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                roleManager.CreateAsync(new IdentityRole(SD.Role_User_Admin)).GetAwaiter().GetResult();
                roleManager.CreateAsync(new IdentityRole(SD.Role_User_Staff)).GetAwaiter().GetResult();
                roleManager.CreateAsync(new IdentityRole(SD.Role_User_Cust)).GetAwaiter().GetResult();

                //for admin default login

                var adminUserEmail = "admin@gmail.com";
                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser
                    {
                        UserName = adminUserEmail,
                        Name = "Admin",
                        IsRegular = true,
                        IsActive = true,
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        Verified = true,
                        PaymentDue = false,
                        PhoneNumber = "9810476969"
                    };
                    await userManager.CreateAsync(newAdminUser, "Admin@2023");

                    await userManager.AddToRoleAsync(newAdminUser, SD.Role_User_Admin);
                }

                //for staff default login

                var customerUserEmail = "staff@gmail.com";
                var customerUser = await userManager.FindByEmailAsync(customerUserEmail);
                if (customerUser == null)
                {
                    var newcustomerUser = new ApplicationUser
                    {
                        UserName = customerUserEmail,
                        Name = "Staff",
                        IsRegular = true,
                        IsActive = true,
                        Email = customerUserEmail,
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        Verified = true,
                        PaymentDue = false,
                        PhoneNumber = "9842176969"
                    };
                    await userManager.CreateAsync(newcustomerUser, "Staff@2023");
                    await userManager.AddToRoleAsync(newcustomerUser, SD.Role_User_Cust);

                }
            }

        }
    }
}
