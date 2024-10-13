using Go1Bet.Core.Entities.Sport;
using Go1Bet.Infrastructure.DTO_s.Sport.FavouriteSportMatch;
using Go1Bet.Infrastructure.DTO_s.Sport.Odd;
using Go1Bet.Infrastructure.DTO_s.Sport.Opponent;
using Go1Bet.Infrastructure.DTO_s.Sport.SportEvent;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Infrastructure.DTO_s.Sport.SportMatch
{
    public class SportMatchItemDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public DateTime DateCreated { get; set; }
        public SportEventItemDTO SportEvent { get; set; }
        public string SportEventId { get; set; }
        public string SportEventName { get; set; }
        public List<OpponentItemDTO> Opponents { get; set; }
        public List<OddItemDTO> Odds { get; set; }
        public double BettingFund { get; set; }
        public int CountBets { get; set; }
        public int CountFavouriteSportMatches { get; set; }
        public List<FavouriteSportMatchItemDTO> FavouriteSportMatches { get; set; } = new List<FavouriteSportMatchItemDTO>();
    }
}
