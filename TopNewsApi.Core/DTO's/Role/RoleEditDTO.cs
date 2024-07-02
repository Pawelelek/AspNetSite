using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopNewsApi.Core.DTO_s.Role
{
    public class RoleEditDTO
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        public string? ConcurrencyStamp { get; set; }
    }

}
