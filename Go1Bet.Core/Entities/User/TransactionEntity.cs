using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Go1Bet.Core.Constants;

namespace Go1Bet.Core.Entities.User
{
    [Table("tbl_Transactions")]
    public class TransactionEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Value {  get; set; }
        [DisplayName("Balance")]
        public BalanceEntity Balance { get; set; }

        [ForeignKey(nameof(Balance))]
        public string? BalanceId { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
