using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Go1Bet.Core.Entities.Tokens;

namespace Go1Bet.Core.Entities.Specifications
{
    public static class RefreshTokenSpecification
    {
        public class GetRefreshToken : Specification<RefreshToken>
        {
            public GetRefreshToken(string refreshToken)
            {
                Query.Where(t => t.Token == refreshToken);
            }
        }

        public class GetAllTokens : Specification<RefreshToken>
        {
            public GetAllTokens(string userId)
            {
                Query.Where(t => t.UserId == userId);
            }
        }
    }
}
