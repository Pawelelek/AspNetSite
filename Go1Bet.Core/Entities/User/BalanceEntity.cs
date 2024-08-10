using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Core.Entities.User
{
    [Table("tbl_Balance")]
    public class BalanceEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public string Money { get; set; }
        public bool Reviewed { get; set; } = false;
        [DisplayName("User")]
        public AppUser User { get; set; }
        [ForeignKey(nameof(User))]
        public string? UserId { get; set; }
        public ICollection<TransactionEntity> TransactionHistory { get; set; } = new List<TransactionEntity>();
    }
}
