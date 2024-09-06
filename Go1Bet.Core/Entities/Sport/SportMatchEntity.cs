using Go1Bet.Core.Entities.Bonuses;
using Go1Bet.Core.Entities.User;
using System;
using System.Collections.Generic;
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
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public SportEventEntity SportEvent { get; set; }
        [ForeignKey(nameof(SportEvent))]
        public string? SportEventId { get; set; }

        //Опоненти
        public OpponentEntity FirstOpponent { get; set; }
        [ForeignKey(nameof(FirstOpponent))]
        public string? FirstTeamId { get; set; }
        public OpponentEntity SecondOpponent { get; set; }
        [ForeignKey(nameof(SecondOpponent))]
        public string? SecondTeamId { get; set; }

        //Коеф на перемогу
        public double FirstOponentWinRate { get; set; }
        public double SecondOponentWinRate { get; set; }
        public double DrawWinRating { get; set; }

        //Ставки
        public double BettingFund { get; set; }
        public int CountBets { get; set; }


        public ICollection<AppUser> UsersParticipation { get; set; }
        public ICollection<BalanceEntity> BalancesParticipation { get; set; }
    }
}
