using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TopNewsApi.Core.Entities.Tokens;
using TopNewsApi.Core.Entities.User;

namespace TopNewsApi.Infrastructure.Context
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext() : base() { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }
    }
}
