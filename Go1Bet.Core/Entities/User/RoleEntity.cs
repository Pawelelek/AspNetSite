using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Core.Entities.User
{
    public class RoleEntity : IdentityRole<string>
    {
        public virtual ICollection<UserRoleEntity> UserRoles { get; set; }
    }
}
