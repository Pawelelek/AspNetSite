using Go1Bet.Core.Constants;
using Go1Bet.Core.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Core.DTO_s.User
{
    public class TransactionItemDTO
    {
        public string Id { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public string MoneyState { get; set; }
        public string? BalanceId { get; set; }
        public string TransactionType { get; set; }
        public string DateCreated { get; set; }
    }
}
