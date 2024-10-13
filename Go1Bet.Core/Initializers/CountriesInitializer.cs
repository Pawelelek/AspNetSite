using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Core.Initializers
{
    using global::Go1Bet.Core.Context;
    using global::Go1Bet.Core.Constants;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using global::Go1Bet.Core.Entities.Sport;

    namespace Go1Bet.Core.Initializers
    {
        public static class CountriesInitializer
        {
            public static async Task SeedCountries(IApplicationBuilder applicationBuilder)
            {
                using (var serviceScope = applicationBuilder.ApplicationServices.CreateAsyncScope())
                {
                    var context = serviceScope.ServiceProvider.GetService<AppDbContext>();


                    if (!context.Countries.Any())
                    {
                        foreach (var countryCode in CountryCodes.countryCodes)
                        {
                            await context.AddAsync(new CountryEntity() { Name = countryCode });
                        }
                        await context.SaveChangesAsync();
                    }
                    
                }
            }
        }
    }

}
