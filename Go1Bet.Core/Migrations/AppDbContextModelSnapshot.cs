﻿// <auto-generated />
using System;
using Go1Bet.Core.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Go1Bet.Core.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Go1Bet.Core.Entities.Bonuses.PromocodeEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("CountAvailable")
                        .HasColumnType("integer");

                    b.Property<int>("CountEntries")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Key")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<double>("PriceMoney")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Promocodes");
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.Bonuses.PromocodeUserEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PromocodeId")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PromocodeId");

                    b.HasIndex("UserId");

                    b.ToTable("UserPromocodes");
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.Category.CategoryEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasMaxLength(255)
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(4000)
                        .HasColumnType("character varying(4000)");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("ParentId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("tbl_Categories");
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.Sport.BetEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("BetTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("OddId")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OddId");

                    b.HasIndex("UserId");

                    b.ToTable("Bets");
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.Sport.CountryEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.Sport.FavouriteSportMatch", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("SportMatchId")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SportMatchId");

                    b.HasIndex("UserId");

                    b.ToTable("FavouriteSportMatches");
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.Sport.OddEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("OpponentId")
                        .HasColumnType("text");

                    b.Property<string>("SportMatchId")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("OpponentId");

                    b.HasIndex("SportMatchId");

                    b.ToTable("Odds");
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.Sport.OpponentEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("CountryCode")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("SportMatchId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SportMatchId");

                    b.ToTable("Opponents");
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.Sport.PersonEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Number")
                        .HasColumnType("text");

                    b.Property<string>("OpponentId")
                        .HasColumnType("text");

                    b.Property<string>("Position")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OpponentId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.Sport.SportEventEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(4000)
                        .HasColumnType("character varying(4000)");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("ParentId")
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("SportEvents");
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.Sport.SportMatchEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("SportEventId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SportEventId");

                    b.ToTable("SportMatches");
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.Tokens.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ExpireDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsRevoked")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("boolean");

                    b.Property<string>("JwtId")
                        .HasColumnType("text");

                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshToken");
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.User.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateLastEmailUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateLastPasswordUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateLastPersonalInfoUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .HasColumnType("text");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsGoogle")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PasswordResetCode")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("RefUserId")
                        .HasColumnType("text");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<string>("SportMatchEntityId")
                        .HasColumnType("text");

                    b.Property<string>("SwitchedBalanceId")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.HasIndex("RefUserId");

                    b.HasIndex("SportMatchEntityId");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.User.BalanceEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Money")
                        .HasColumnType("double precision");

                    b.Property<bool>("Reviewed")
                        .HasColumnType("boolean");

                    b.Property<string>("SportMatchEntityId")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SportMatchEntityId");

                    b.HasIndex("UserId");

                    b.ToTable("tbl_Balance");
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.User.RoleEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.User.TransactionEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("BalanceId")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("Discount")
                        .HasColumnType("integer");

                    b.Property<int>("TransactionType")
                        .HasColumnType("integer");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("BalanceId");

                    b.ToTable("tbl_Transactions");
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.User.UserRoleEntity", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.Bonuses.PromocodeUserEntity", b =>
                {
                    b.HasOne("Go1Bet.Core.Entities.Bonuses.PromocodeEntity", "Promocode")
                        .WithMany("PromocodeUsers")
                        .HasForeignKey("PromocodeId");

                    b.HasOne("Go1Bet.Core.Entities.User.AppUser", "User")
                        .WithMany("PromocodeUsers")
                        .HasForeignKey("UserId");

                    b.Navigation("Promocode");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.Category.CategoryEntity", b =>
                {
                    b.HasOne("Go1Bet.Core.Entities.Category.CategoryEntity", "Parent")
                        .WithMany("Subcategories")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.Sport.BetEntity", b =>
                {
                    b.HasOne("Go1Bet.Core.Entities.Sport.OddEntity", "Odd")
                        .WithMany("Bets")
                        .HasForeignKey("OddId");

                    b.HasOne("Go1Bet.Core.Entities.User.AppUser", "User")
                        .WithMany("Bets")
                        .HasForeignKey("UserId");

                    b.Navigation("Odd");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.Sport.FavouriteSportMatch", b =>
                {
                    b.HasOne("Go1Bet.Core.Entities.Sport.SportMatchEntity", "SportMatch")
                        .WithMany("FavouriteSportMatches")
                        .HasForeignKey("SportMatchId");

                    b.HasOne("Go1Bet.Core.Entities.User.AppUser", "User")
                        .WithMany("FavouriteSportMatches")
                        .HasForeignKey("UserId");

                    b.Navigation("SportMatch");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.Sport.OddEntity", b =>
                {
                    b.HasOne("Go1Bet.Core.Entities.Sport.OpponentEntity", "Opponent")
                        .WithMany()
                        .HasForeignKey("OpponentId");

                    b.HasOne("Go1Bet.Core.Entities.Sport.SportMatchEntity", "SportMatch")
                        .WithMany("Odds")
                        .HasForeignKey("SportMatchId");

                    b.Navigation("Opponent");

                    b.Navigation("SportMatch");
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.Sport.OpponentEntity", b =>
                {
                    b.HasOne("Go1Bet.Core.Entities.Sport.SportMatchEntity", "SportMatch")
                        .WithMany("Opponents")
                        .HasForeignKey("SportMatchId");

                    b.Navigation("SportMatch");
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.Sport.PersonEntity", b =>
                {
                    b.HasOne("Go1Bet.Core.Entities.Sport.OpponentEntity", "Opponent")
                        .WithMany("Teammates")
                        .HasForeignKey("OpponentId");

                    b.Navigation("Opponent");
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.Sport.SportEventEntity", b =>
                {
                    b.HasOne("Go1Bet.Core.Entities.Category.CategoryEntity", "Parent")
                        .WithMany("SportEvents")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.Sport.SportMatchEntity", b =>
                {
                    b.HasOne("Go1Bet.Core.Entities.Sport.SportEventEntity", "SportEvent")
                        .WithMany("SportMatches")
                        .HasForeignKey("SportEventId");

                    b.Navigation("SportEvent");
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.Tokens.RefreshToken", b =>
                {
                    b.HasOne("Go1Bet.Core.Entities.User.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.User.AppUser", b =>
                {
                    b.HasOne("Go1Bet.Core.Entities.User.AppUser", "RefUser")
                        .WithMany("RefUsers")
                        .HasForeignKey("RefUserId");

                    b.HasOne("Go1Bet.Core.Entities.Sport.SportMatchEntity", null)
                        .WithMany("UsersParticipation")
                        .HasForeignKey("SportMatchEntityId");

                    b.Navigation("RefUser");
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.User.BalanceEntity", b =>
                {
                    b.HasOne("Go1Bet.Core.Entities.Sport.SportMatchEntity", null)
                        .WithMany("BalancesParticipation")
                        .HasForeignKey("SportMatchEntityId");

                    b.HasOne("Go1Bet.Core.Entities.User.AppUser", "User")
                        .WithMany("Balances")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.User.TransactionEntity", b =>
                {
                    b.HasOne("Go1Bet.Core.Entities.User.BalanceEntity", "Balance")
                        .WithMany("TransactionHistory")
                        .HasForeignKey("BalanceId");

                    b.Navigation("Balance");
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.User.UserRoleEntity", b =>
                {
                    b.HasOne("Go1Bet.Core.Entities.User.RoleEntity", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Go1Bet.Core.Entities.User.AppUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Go1Bet.Core.Entities.User.RoleEntity", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Go1Bet.Core.Entities.User.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Go1Bet.Core.Entities.User.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Go1Bet.Core.Entities.User.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.Bonuses.PromocodeEntity", b =>
                {
                    b.Navigation("PromocodeUsers");
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.Category.CategoryEntity", b =>
                {
                    b.Navigation("SportEvents");

                    b.Navigation("Subcategories");
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.Sport.OddEntity", b =>
                {
                    b.Navigation("Bets");
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.Sport.OpponentEntity", b =>
                {
                    b.Navigation("Teammates");
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.Sport.SportEventEntity", b =>
                {
                    b.Navigation("SportMatches");
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.Sport.SportMatchEntity", b =>
                {
                    b.Navigation("BalancesParticipation");

                    b.Navigation("FavouriteSportMatches");

                    b.Navigation("Odds");

                    b.Navigation("Opponents");

                    b.Navigation("UsersParticipation");
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.User.AppUser", b =>
                {
                    b.Navigation("Balances");

                    b.Navigation("Bets");

                    b.Navigation("FavouriteSportMatches");

                    b.Navigation("PromocodeUsers");

                    b.Navigation("RefUsers");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.User.BalanceEntity", b =>
                {
                    b.Navigation("TransactionHistory");
                });

            modelBuilder.Entity("Go1Bet.Core.Entities.User.RoleEntity", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
