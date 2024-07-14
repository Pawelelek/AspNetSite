using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Go1Bet.Core.Constants;
using Go1Bet.Core.Entities.User;
using Go1Bet.Infrastructure.Context;

namespace Go1Bet.Infrastructure.Initializers
{
    public static class UsersAndRolesInitializer
    {
        public static async Task SeedUsersAndRoles(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateAsyncScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                UserManager<AppUser> userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                if (userManager.FindByEmailAsync("admin@email.com").Result == null)
                {
                    AppUser admin = new AppUser()
                    {
                        UserName = "admin@email.com",
                        FirstName = "Pasha",
                        LastName = "Panchuk",
                        Email = "admin@email.com",
                        EmailConfirmed = true,
                        PhoneNumber = "+xx(xxx)xxx-xx-xx",
                        PhoneNumberConfirmed = true
                    };

                    AppUser user = new AppUser()
                    {
                        UserName = "user@email.com",
                        FirstName = "Artem",
                        LastName = "Slobodeniuk",
                        Email = "user@email.com",
                        EmailConfirmed = false,
                        PhoneNumber = "+38(099)999-99-99",
                        PhoneNumberConfirmed = true,
                    };

                    context.Roles.AddRange(
                        new IdentityRole()
                        {
                            Name = Roles.Admin,
                            NormalizedName = Roles.Admin.ToUpper(),
                        },
                        new IdentityRole()
                        {
                            Name = Roles.User,
                            NormalizedName = Roles.User.ToUpper(),
                        });
                    await context.SaveChangesAsync();

                    IdentityResult adminResult = userManager.CreateAsync(admin, "Qwerty-1").Result;
                    if (adminResult.Succeeded)
                    {
                        userManager.AddToRoleAsync(admin, Roles.Admin).Wait();
                    }
                    IdentityResult userResult = userManager.CreateAsync(user, "Qwerty-1").Result;
                    if (userResult.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, Roles.User).Wait();
                    }
                }
            }
        }
    }
}
