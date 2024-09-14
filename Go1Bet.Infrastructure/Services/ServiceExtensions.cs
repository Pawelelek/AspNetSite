using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Go1Bet.Infrastructure.Services;
using Go1Bet.Core.Context;
using Go1Bet.Core.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Go1Bet.Infrastructure.AutoMapper;
using Go1Bet.Core.Repository;
using Go1Bet.Infrastructure.Services.SportService;

namespace Go1Bet.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddTransient<UserService>();
            services.AddTransient<RoleService>();
            services.AddTransient<JwtService>();
            services.AddTransient<EmailService>();
            services.AddTransient<BalanceService>();
            services.AddTransient<BonusService>();
            services.AddTransient<CategoryService>();
            services.AddTransient<PersonService>();
            services.AddTransient<OpponentService>();
            services.AddTransient<SportMatchService>();
            services.AddTransient<SportEventService>();
        }

        public static void AddMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AppMapProfile));
        }
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
            services.AddIdentity<AppUser, RoleEntity>(options =>
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
