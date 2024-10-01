using Go1Bet.Core.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Core.Entities.Sport
{
    public class BetEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public OddEntity Odd { get; set; }

        [ForeignKey(nameof(Odd))]
        public string OddId { get; set; }
        public double Amount { get; set; } // Сума ставки
        public DateTime BetTime { get; set; } = DateTime.UtcNow;// Час здійснення ставки
        public AppUser User { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; } // ID користувача
    }
}
