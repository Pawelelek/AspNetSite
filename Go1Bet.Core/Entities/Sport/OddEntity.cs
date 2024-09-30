using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Go1Bet.Core.Entities.Sport
{
    public class OddEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public SportMatchEntity SportMatch { get; set; }

        [ForeignKey(nameof(SportMatch))]
        public string SportMatchId { get; set; }
        public OpponentEntity Opponent { get; set; } // Опонент, якщо це ставка на перемогу конкретної команди

        [ForeignKey(nameof(Opponent))]
        public string? OpponentId { get; set; } // ID опонента (якщо ставка на перемогу)

        public decimal Value { get; set; } // коефіцієнт
        public string Type { get; set; } // "win_team1", "win_team2", "draw"
        public ICollection<BetEntity> Bets { get; set; }
    }
}
