using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Infrastructure.DTO_s.Sport.Odd
{
    public class OddEditDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SportMatchId { get; set; }
        public string? OpponentId { get; set; } // ID опонента (якщо ставка на перемогу)

        public decimal Value { get; set; } // коефіцієнт
        public string Type { get; set; } // "win_team1", "win_team2", "draw"
    }
}
