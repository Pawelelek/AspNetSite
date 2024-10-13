using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Infrastructure.DTO_s.Sport.Person
{
    public class PersonEditDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; } // Тренер, Помічник, Лікар, Воротарь, Нападник
        public string Number { get; set; }
        public string? OpponentId { get; set; }
    }
}
