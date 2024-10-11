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
    public static class SportApiInitializer
    {
        public static async Task SeedSportApi(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateAsyncScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();


                if (!context.Promocodes.Any())
                {
                    #region Events


                    string a = "123";
                    string b = "123";


                    #endregion

                    #region Matches


                    string a1 = "123";
                    string b1 = "123";


                    #endregion

                    #region Opponents


                    string a2 = "123";
                    string b2 = "123";


                    #endregion

                    #region Odds


                    string a3 = "123";
                    string b3 = "123";


                    #endregion
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
