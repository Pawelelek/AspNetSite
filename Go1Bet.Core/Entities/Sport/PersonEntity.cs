using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Core.Entities.Sport
{
    public class PersonEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Position { get; set; } // Тренер, Помічник, Лікар, Воротарь, Нападник
        [DisplayName("Opponent")]
        public OpponentEntity Opponent { get; set; }
        [ForeignKey(nameof(Opponent))]
        public string? OpponentId { get; set; }
    }
}
