using Go1Bet.Core.DTO_s.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Core.DTO_s.Bonus
{
    public class PromocodeItemDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DateCreated { get; set; }
        public string ExpirationDate { get; set; }
        public string Key { get; set; }
        public double PriceMoney { get; set; }
        public int CountAvailable { get; set; }
        public int CountEntries { get; set; }
        public List<UserItemDTO> Users { get; set; }
    }
}
