using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Infrastructure.DTO_s.User
{
    public class BalanceItemDTO
    {
        public string Id { get; set; }
        public string Money { get; set; }
        public bool Reviewed { get; set; }
        public string DateCreated { get; set; }
        public string UserId { get; set; }
        public int countTransactions { get; set; }
        public List<TransactionItemDTO> Transactions { get; set; }
    }
}
