using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNewsApi.Core.Entities.User;
using TopNewsApi.Infrastructure.Context;

namespace TopNewsApi.Infrastructure.Initializers
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
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        },
                        new IdentityRole()
                        {
                            Name = "User",
                            NormalizedName = "USER"
                        });
                    await context.SaveChangesAsync();

                    IdentityResult adminResult = userManager.CreateAsync(admin, "Qwerty-1").Result;
                    if (adminResult.Succeeded)
                    {
                        userManager.AddToRoleAsync(admin, "Administrator").Wait();
                    }
                    IdentityResult userResult = userManager.CreateAsync(user, "Qwerty-1").Result;
                    if (userResult.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "User").Wait();
                    }
                }
            }
        }
    }
}
