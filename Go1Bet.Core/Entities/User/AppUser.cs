using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public virtual ICollection<BalanceEntity> Balances { get; set; }
        public virtual ICollection<UserRoleEntity> UserRoles { get; set; }

        //Bonuses > Money (Gift) , Bet insurance , Promocode, bool Reviewed = false

        //BettingHistory > id , userId , The Name of the sport , period

        //List of messages > all, output, incoming > Received from , Send to, Message, bool Reviewed = false

        //Favorite SportEvents > Competition , SportEvent


        // ================== Sport Event =====================

        //Country (UA, USA, UK, DE, PL ...)

        //TypeTeam()
        //Team (Type, Name, ValuePerson, Country)

        //SportEvent > Type, Bet(TypeBet (Coef), ValueBet, Possible Win(ValueBet*Coeficient)), Teams
        
    }
}
