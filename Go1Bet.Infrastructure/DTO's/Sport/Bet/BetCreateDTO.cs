using Go1Bet.Infrastructure.DTO_s.Sport.Odd;
using Go1Bet.Infrastructure.DTO_s.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Infrastructure.DTO_s.Sport.Bet
{
    public class BetCreateDTO
    {
        public string OddId { get; set; }
        public double Amount { get; set; } // Сума ставки
        public string UserId { get; set; } // ID користувача
    }
}
