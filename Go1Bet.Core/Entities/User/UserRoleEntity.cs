
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Core.Entities.User
{
    public class UserRoleEntity : IdentityUserRole<string>
    {
        public virtual AppUser User { get; set; }
        public virtual RoleEntity Role { get; set; }
    }
}
