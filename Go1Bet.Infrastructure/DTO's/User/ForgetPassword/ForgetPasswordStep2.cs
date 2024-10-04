using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Infrastructure.DTO_s.User.ForgetPassword
{
    public class ForgetPasswordStep2
    {
        public string Email { get; set; }
        public string ReceivedCodeFromEmail { get; set; }
    }
}
