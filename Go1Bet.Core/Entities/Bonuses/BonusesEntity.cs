using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Core.Entities.Bonuses
{
    public class BonusesEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public BonusUserEntity Exercise { get; set; }
        public PromocodeUserEntity Promocode { get; set; }
    }
}
