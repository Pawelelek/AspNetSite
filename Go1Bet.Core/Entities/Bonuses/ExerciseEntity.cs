using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Core.Entities.Bonuses
{
    public class ExerciseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime ExpirationDate { get; set; }
        public double PriceMoney { get; set; }
        public int CountEntries { get; set; }
    }
}
