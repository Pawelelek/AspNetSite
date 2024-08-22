using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Core.DTO_s.Bonus.Promocode
{
    public class PromocodeEditDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Key { get; set; }
        public double PriceMoney { get; set; }
        public int CountAvailable { get; set; }
    }
}
