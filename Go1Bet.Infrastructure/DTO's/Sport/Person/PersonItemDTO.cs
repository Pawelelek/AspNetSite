using Go1Bet.Core.Entities.Sport;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Infrastructure.DTO_s.Sport.Person
{
    public class PersonItemDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; } // Тренер, Помічник, Лікар, Воротарь, Нападник        
        public string? OpponentId { get; set; }
        public string OpponentName { get; set; }
    }
}
