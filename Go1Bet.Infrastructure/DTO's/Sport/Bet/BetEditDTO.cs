using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Infrastructure.DTO_s.Sport.Bet
{
    public class BetEditDTO
    {
        public string Id { get; set; }
        public string OddId { get; set; }
        public double Amount { get; set; } // Сума ставки
        public DateTime BetTime { get; set; } // Час здійснення ставки
        public string UserId { get; set; } // ID користувача
    }
}
