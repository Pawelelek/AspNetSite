using Go1Bet.Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Infrastructure.DTO_s.Sport.Opponent
{
    public class OpponentCreateDTO
    {
        public string Name { get; set; }
        public string SportMatchId { get; set; }
        public string CountryCode { get; set; }
    }
}
