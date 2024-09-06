using Go1Bet.Core.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Core.Entities.Sport
{
    public class OpponentEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        //public ICollection<PersoneEntity> Trainers { get; set; }
        public ICollection<PersonEntity> Teammates { get; set; }
        [StringLength(4000)]
        public string Description { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
