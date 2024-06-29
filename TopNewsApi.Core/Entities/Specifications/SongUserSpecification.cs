using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using TopNewsApi.Core.Entities.User;

namespace TopNewsApi.Core.Entities.Specifications
{
    public static class SongUserSpecification
    {
        public class GetByUserId : Specification<SongUser>
        {
            public GetByUserId(string userId)
            {
                Query.Where(x => x.UserId == userId);
            }
        }
    }
}
