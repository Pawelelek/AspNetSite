using Go1Bet.Core.Constants;
using Go1Bet.Core.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Go1Bet.Core.Entities.Sport
{
    public class OpponentEntity
    { 
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public SportMatchEntity SportMatch { get; set; }
        [ForeignKey(nameof(SportMatch))]
        public string SportMatchId { get; set; }
        public string CountryCode { get; set; }
        public ICollection<PersonEntity> Teammates { get; set; }
    }
}
