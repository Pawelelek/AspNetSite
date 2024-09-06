using Go1Bet.Core.Entities.Category;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Core.Entities.Sport
{
    public class SportEventEntity //Example: Euro2024
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        [StringLength(4000)]
        public string Description { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        //Foreign keys:
        public ICollection<SportMatchEntity> SportMatches { get; set; } = new List<SportMatchEntity>();
    }
}
