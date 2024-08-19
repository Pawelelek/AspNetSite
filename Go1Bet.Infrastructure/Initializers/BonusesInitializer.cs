using Go1Bet.Core.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Infrastructure.Initializers
{
    public static class BonusesInitializer
    {
        public static async Task SeedBonuses(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateAsyncScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();


                if (!context.Categories.Any())
                {
                    //var cat1 = new CategoryEn
                    //context.Categories.Add(cat1);
                    //context.SaveChanges();
                }
            }
        }
    }
}
