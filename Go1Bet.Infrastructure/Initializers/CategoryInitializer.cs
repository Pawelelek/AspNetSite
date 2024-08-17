using Go1Bet.Core.Constants;
using Go1Bet.Core.Context;
using Go1Bet.Core.Entities.User;
using Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using static System.Formats.Asn1.AsnWriter;

namespace Go1Bet.Infrastructure.Initializers
{
    public static class CateogryInitializer
    {
        public static async Task SeedCategories(IApplicationBuilder applicationBuilder)
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
