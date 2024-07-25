using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Core.Entities.User
{
    public class AppUser : IdentityUser<string>
    {
        public bool IsGoogle { get; set; } = false;
        public bool IsDelete { get; set; } = false;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        //make a migration
        public DateTime DateLastPasswordUpdated { get; set; } = DateTime.UtcNow;
        public DateTime DateLastEmailUpdated { get; set; } = DateTime.UtcNow;
        public DateTime DateLastPersonalInfoUpdated { get; set; } = DateTime.UtcNow;
        public virtual ICollection<UserRoleEntity> UserRoles { get; set; }
    }
}
