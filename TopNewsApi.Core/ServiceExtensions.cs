using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNewsApi.Core.AutoMapper.Songs;
using TopNewsApi.Core.AutoMapper.User;
using TopNewsApi.Core.Interfaces;
using TopNewsApi.Core.Services;

namespace TopNewsApi.Core
{
    public static class ServiceExtensions
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddTransient<UserService>();
            services.AddScoped<ISongService, SongService>();
            services.AddScoped<ISongUserService, SongUserService>();
            services.AddTransient<JwtService>();
        }

        public static void AddMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperUserProfile));
            services.AddAutoMapper(typeof(AutoMapperSong));
            services.AddAutoMapper(typeof(AutoMapperSongUser));
        }
    }
}
