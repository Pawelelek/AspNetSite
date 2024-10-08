using Go1Bet.Core.Entities.Sport;
using Go1Bet.Infrastructure.DTO_s.Sport.SportMatch;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Infrastructure.DTO_s.Sport.SportEvent
{
    public class SportEventItemDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public DateTime DateCreated { get; set; }
        public string Status { get; set; }
        public int countSportMatches { get; set; }
        public string? ParentId { get; set; }
        public string ParentName { get; set; }
        public List<SportMatchItemDTO> SportMatches { get; set; }
    }
}
