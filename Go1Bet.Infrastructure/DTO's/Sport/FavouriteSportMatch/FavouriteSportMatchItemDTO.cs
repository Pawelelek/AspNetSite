using Go1Bet.Infrastructure.DTO_s.Sport.SportMatch;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Infrastructure.DTO_s.Sport.FavouriteSportMatch
{
    public class FavouriteSportMatchItemDTO
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string SportMatchId { get; set; }
        public string SportMatchName { get; set; }
        public SportMatchItemDTO SportMatch { get; set; }
    }
}
