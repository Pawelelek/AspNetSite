using Go1Bet.Core.Entities.Sport;
using Go1Bet.Core.Entities.User;
using Go1Bet.Infrastructure.DTO_s.Sport.Odd;
using Go1Bet.Infrastructure.DTO_s.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Infrastructure.DTO_s.Sport.Bet
{
    public class BetItemDTO
    {
        public string Id { get; set; }
        public string OddName { get; set; }
        public string OddId { get; set; }
        public double Amount { get; set; } // Сума ставки
        public decimal Value { get; set; }
        public DateTime BetTime { get; set; } // Час здійснення ставки
        public string UserName { get; set; }
        public string UserId { get; set; } // ID користувача
    }
}
