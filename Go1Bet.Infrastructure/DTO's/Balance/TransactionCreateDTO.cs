using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Infrastructure.DTO_s.Balance
{
    public class TransactionCreateDTO
    {
        public string Value { get; set; }
        public string BalanceId { get; set; }
        public string TransactionType { get; set; }
    }
}
