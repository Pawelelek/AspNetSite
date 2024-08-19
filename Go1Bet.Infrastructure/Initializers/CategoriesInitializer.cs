using Go1Bet.Core.Constants;
using Go1Bet.Core.Context;
using Go1Bet.Core.Entities.Category;
using Go1Bet.Core.Entities.User;
using Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using static System.Formats.Asn1.AsnWriter;

namespace Go1Bet.Infrastructure.Initializers
{
    public static class CateogoriesInitializer
    {
        public static async Task SeedCategories(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateAsyncScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();


                if (!context.Categories.Any())
                {
                    CategoryEntity cat1 = new CategoryEntity()
                    {
                        Name = "Спорт",
                        ParentId = null,
                        DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    };
                    context.Categories.Add(cat1);
                    context.SaveChanges();

                    CategoryEntity cat2 = new CategoryEntity()
                    {
                        Name = "Кіберспорт",
                        ParentId = null,
                        DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    };
                    context.Categories.Add(cat2);
                    context.SaveChanges();

                    CategoryEntity cat3 = new CategoryEntity()
                    {
                        Name = "Футбол",
                        ParentId = cat1.Id,
                        DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    };
                    context.Categories.Add(cat3);
                    context.SaveChanges();
                    CategoryEntity cat4 = new CategoryEntity()
                    {
                        Name = "CS:GO",
                        ParentId = cat2.Id,
                        DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    };
                    context.Categories.Add(cat4);
                    context.SaveChanges();
                }
            }
        }
    }
}
