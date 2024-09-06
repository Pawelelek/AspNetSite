using Go1Bet.Core.Context;
using Go1Bet.Core.Entities.Bonuses;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Core.Initializers
{
    public static class BonusesInitializer
    {
        public static async Task SeedBonuses(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateAsyncScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();


                if (!context.Promocodes.Any())
                {
                    var promo1 = new PromocodeEntity()
                    {
                        Name = "START",
                        Key = "START",
                        CountAvailable = 100,
                        PriceMoney = 1000,
                        ExpirationDate = DateTime.UtcNow.AddYears(100)
                    };
                    var promo2 = new PromocodeEntity()
                    {
                        Name = "TEST1",
                        Key = "TEST1",
                        CountAvailable = 1,
                        PriceMoney = 1000,
                        ExpirationDate = DateTime.UtcNow.AddYears(50)
                    };
                    var promo3 = new PromocodeEntity()
                    {
                        Name = "TEST2",
                        Key = "TEST2",
                        CountAvailable = 2,
                        PriceMoney = 1000,
                        ExpirationDate = DateTime.UtcNow.AddYears(5)
                    };
                    await context.Promocodes.AddAsync(promo1);
                    await context.Promocodes.AddAsync(promo2);
                    await context.Promocodes.AddAsync(promo3);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
