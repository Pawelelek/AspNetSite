using Go1Bet.Core.Constants;
using Go1Bet.Core.Context;
using Go1Bet.Core.Entities.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Core.Initializers
{
    public static class IdentitiesInitializer
    {
        public static async Task SeedIdentities(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateAsyncScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                UserManager<AppUser> userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                RoleManager<RoleEntity> roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<RoleEntity>>();

                if (!userManager.Users.Any())
                {
                    AppUser admin1 = new AppUser()
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = "admin@email.com",
                        FirstName = "Pasha",
                        LastName = "Panchuk",
                        Email = "admin@email.com",
                        EmailConfirmed = true,
                        PhoneNumber = "+xx(xxx)xxx-xx-xx",
                        PhoneNumberConfirmed = true
                    };

                    AppUser admin2 = new AppUser()
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = "user@email.com",
                        FirstName = "Artem",
                        LastName = "Slobodeniuk",
                        Email = "user@email.com",
                        EmailConfirmed = false,
                        PhoneNumber = "+38(099)999-99-99",
                        PhoneNumberConfirmed = true,
                    };
                    if (!roleManager.Roles.Any())
                    {
                        foreach (var role in Roles.All)
                        {
                            var result = roleManager.CreateAsync(new RoleEntity
                            {
                                Id = Guid.NewGuid().ToString(),
                                Name = role
                            }).Result;
                        }
                    }
                    var balance1 = new BalanceEntity() { Money = 10000, UserId = admin1.Id };
                    admin1.SwitchedBalanceId = balance1.Id;

                    var balance2 = new BalanceEntity() { Money = 10000, UserId = admin2.Id };
                    admin2.SwitchedBalanceId = balance2.Id;

                    IdentityResult adminResult = userManager.CreateAsync(admin1, "Qwerty-1").Result;
                    if (adminResult.Succeeded)
                    {
                        userManager.AddToRoleAsync(admin1, Roles.Admin).Wait();
                    }
                    IdentityResult userResult = userManager.CreateAsync(admin2, "Qwerty-1").Result;
                    if (userResult.Succeeded)
                    {
                        userManager.AddToRoleAsync(admin2, Roles.Admin).Wait();
                    }

                    await context.Balances.AddAsync(balance1);
                    await context.Balances.AddAsync(balance2);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
