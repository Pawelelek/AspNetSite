using Go1Bet.Core.Entities.Bonuses;
using Go1Bet.Core.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Core.Entities.Sport
{
    public class SportMatchEntity //Ukraine : Romanian
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public SportEventEntity SportEvent { get; set; }

        [ForeignKey(nameof(SportEvent))]
        public string? SportEventId { get; set; }

        public ICollection<OpponentEntity> Opponents { get; set; }
        public ICollection<OddEntity> Odds { get; set; }

        public ICollection<AppUser> UsersParticipation { get; set; }
        public ICollection<BalanceEntity> BalancesParticipation { get; set; }
        public ICollection<FavouriteSportMatch> FavouriteSportMatches { get; set; } = new List<FavouriteSportMatch>();
    }
}
