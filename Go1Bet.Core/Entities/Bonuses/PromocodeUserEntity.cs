using Go1Bet.Core.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Core.Entities.Bonuses
{
    public class PromocodeUserEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [DisplayName("User")]
        public AppUser User { get; set; }
        [ForeignKey(nameof(User))]
        public string? UserId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
