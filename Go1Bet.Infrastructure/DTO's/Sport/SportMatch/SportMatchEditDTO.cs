using Go1Bet.Infrastructure.DTO_s.Sport.Opponent;
using Go1Bet.Infrastructure.DTO_s.Sport.SportEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Infrastructure.DTO_s.Sport.SportMatch
{
    public class SportMatchEditDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string? SportEventId { get; set; }
        public string? FirstOpponentId { get; set; }
        public string? SecondOpponentId { get; set; }
    }
}
