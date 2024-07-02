using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopNewsApi.Core.Constants
{
    public static class Roles
    {
        public static List<string> All = new()
        {
            Admin,
            User,
        };

        public const string Admin = "Administrator";
        public const string User = "User";
    }
}
