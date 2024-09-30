using Go1Bet.Infrastructure.DTO_s.Sport.Opponent;
using Go1Bet.Infrastructure.DTO_s.Sport.SportMatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Infrastructure.DTO_s.Sport.Odd
{
    public class OddCreateDTO
    {
        public string Name { get; set; }
        public string SportMatchId { get; set; }
        public string? OpponentId { get; set; } // ID опонента (якщо ставка на перемогу)

        public decimal Value { get; set; } // коефіцієнт
        public string Type { get; set; } // "win_team1", "win_team2", "draw"
    }
}
