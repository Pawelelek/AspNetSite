using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Infrastructure.DTO_s.Balance
{
    public class BalanceInteractionDTO
    {
        public string BalanceId { get; set; }
        public double Money { get; set; }
        public int Discount { get; set; } = 0;
        public double BonusMoney { get; set; } = 0; //Money + BoneyMoney
    }
}
