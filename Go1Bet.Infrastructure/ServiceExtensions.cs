using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Go1Bet.Core.Entities.User;
using Go1Bet.Core.Interfaces;
using Go1Bet.Infrastructure.Context;
using Go1Bet.Infrastructure.Repository;

namespace Go1Bet.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(connectionString);
                opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
        }


        public static void AddInfrastructureService(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
