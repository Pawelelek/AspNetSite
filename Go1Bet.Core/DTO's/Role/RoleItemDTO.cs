using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Core.DTO_s.Role
{
    public class RoleItemDTO
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        public string? ConcurrencyStamp { get; set; }
    }
}
