using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Go1Bet.Core.Entities.User;
using Microsoft.AspNetCore.Identity;
using Go1Bet.Core.Entities.Category;
using Go1Bet.Core.Entities.Bonuses;
using Go1Bet.Core.Entities.Sport;
using Go1Bet.Core.Entities.Tokens;

namespace Go1Bet.Core.Context
{
    public class AppDbContext : IdentityDbContext<
           AppUser, RoleEntity, string, IdentityUserClaim<string>,
           UserRoleEntity, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>
        >
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserRoleEntity>(permissions =>
            {
                permissions.HasKey(perm => new { perm.UserId, perm.RoleId });
                //Role
                permissions.HasOne(perm => perm.Role)
                   .WithMany(perm => perm.UserRoles)
                   .HasForeignKey(perm => perm.RoleId)
                   .IsRequired();

                //User
                permissions.HasOne(perm => perm.User)
                   .WithMany(perm => perm.UserRoles)
                   .HasForeignKey(perm => perm.UserId)
                   .IsRequired();
            });
        }
        public DbSet<RefreshToken> RefreshToken { get; set; }
        public DbSet<TransactionEntity> Transactions { get; set; }
        public DbSet<BalanceEntity> Balances { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }

        //Bonuses
        public DbSet<PromocodeEntity> Promocodes { get; set; }      
        public DbSet<PromocodeUserEntity> UserPromocodes { get; set; }
        //Sport
        public DbSet<PersonEntity> Persons { get; set; }
        public DbSet<OpponentEntity> Opponents { get; set; }
        public DbSet<SportEventEntity> SportEvents { get; set; }
        public DbSet<SportMatchEntity> SportMatches { get; set; }
        public DbSet<BetEntity> Bets { get; set; }
        public DbSet<OddEntity> Odds { get; set; }
    }
}
