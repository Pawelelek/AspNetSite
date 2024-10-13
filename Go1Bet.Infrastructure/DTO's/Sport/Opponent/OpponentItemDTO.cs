using Go1Bet.Core.Entities.Sport;
using Go1Bet.Infrastructure.DTO_s.Sport.Person;
using Go1Bet.Infrastructure.DTO_s.Sport.SportMatch;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Infrastructure.DTO_s.Sport.Opponent
{
    public class OpponentItemDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public string SportMatchId { get; set; }
        public string SportMatchName { get; set; }
        public string CountryCode { get; set; }
        public List<PersonItemDTO> Teammates { get; set; }
        public int countTeammates { get; set; }
    }
}
