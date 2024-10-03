using Go1Bet.Core.Entities.Sport;
using Go1Bet.Infrastructure.DTO_s.Sport.Bet;
using Go1Bet.Infrastructure.DTO_s.Sport.Opponent;
using Go1Bet.Infrastructure.DTO_s.Sport.SportMatch;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Infrastructure.DTO_s.Sport.Odd
{
    public class OddItemDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SportMatchName { get; set; }
        public string SportMatchId { get; set; }
        public string OpponentName { get; set; } // Опонент, якщо це ставка на перемогу конкретної команди
        public string? OpponentId { get; set; } // ID опонента (якщо ставка на перемогу)

        public decimal Value { get; set; } // коефіцієнт
        public string Type { get; set; } // "win_team1", "win_team2", "draw"
        public List<BetItemDTO> Bets { get; set; }
        public double BettingFund { get; set; }
        // Додаємо властивість для підрахунку суми
        public int CountBets { get; set; }
    }
}
