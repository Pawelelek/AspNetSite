using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Core.DTO_s.User
{
    public class UserEditPasswordDTO
    {
        public string Id { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
