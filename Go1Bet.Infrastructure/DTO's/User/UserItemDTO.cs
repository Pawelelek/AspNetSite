using Go1Bet.Infrastructure.DTO_s.Bonus.Promocode;
using Go1Bet.Infrastructure.DTO_s.Sport.Bet;
using Go1Bet.Infrastructure.DTO_s.Sport.FavouriteSportMatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Infrastructure.DTO_s.User
{
    public class UserItemDTO
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool EmailConfirmed { get; set; }
        public bool IsGoogle { get; set; }
        public bool IsDelete { get; set; }     
        public string Email { get; set; } = string.Empty;
        public string LockedEnd { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string DateCreated { get; set; }
        public string DateLastPasswordUpdated { get; set; }
        public string DateLastEmailUpdated { get; set; }
        public string DateLastPersonalInfoUpdated { get; set; }
        public string SwitchedBalanceId { get; set; }
        public string RefUserId { get; set; }
        public int CountRefUsers { get; set; }
        public int CountFavouriteSportMatches { get; set; }
        public List<UserItemDTO> UsersFromRef { get; set; }
        public List<UserRoleItemDTO> Roles { get; set; }
        public List<BalanceItemDTO> Balances { get; set; }
        public List<PromocodeItemDTO> Promocodes { get; set; }
        public List<FavouriteSportMatchItemDTO> FavouriteSportMatches { get; set; }
    }
}
