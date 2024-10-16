﻿using Go1Bet.Core.Entities.Bonuses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Go1Bet.Core.Entities.Sport;
using Microsoft.Identity.Client;

namespace Go1Bet.Core.Entities.User
{
    public class AppUser : IdentityUser<string>
    {
        public bool IsGoogle { get; set; } = false;
        public bool IsDelete { get; set; } = false;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } = DateTime.UtcNow;
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateLastPasswordUpdated { get; set; } = DateTime.UtcNow;
        public DateTime DateLastEmailUpdated { get; set; } = DateTime.UtcNow;
        public DateTime DateLastPersonalInfoUpdated { get; set; } = DateTime.UtcNow;
        public string SwitchedBalanceId { get; set; }
        public string PasswordResetCode { get; set; }
        public virtual ICollection<BalanceEntity> Balances { get; set; }
        public virtual ICollection<UserRoleEntity> UserRoles { get; set; }
        //Referal
        public AppUser RefUser { get; set; }
        [ForeignKey(nameof(User))]
        public string? RefUserId { get; set; }
        public virtual ICollection<AppUser> RefUsers { get; set; }
        public virtual ICollection<PromocodeUserEntity> PromocodeUsers { get; set; }
        //Bets
        public ICollection<BetEntity> Bets { get; set; }
        public ICollection<FavouriteSportMatch> FavouriteSportMatches { get; set; } = new List<FavouriteSportMatch>();

        // ================== Sport Event =====================

        //Country (UA, USA, UK, DE, PL ...)

        //TypeTeam()

        //Team (Type, Name, ValuePerson, Country)

        //SportEvent > Type, Bet(TypeBet (Coef), ValueBet, Possible Win(ValueBet*Coeficient)), Teams        
    }
}
