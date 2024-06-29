using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNewsApi.Core.Entities.User;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TopNewsApi.Core.Entities.Specifications
{
    public static class SongSpecification
    {
        public class GetBySongName : Specification<Song>
        {
            public GetBySongName(string title)
            {
                Query.Where(x => x.Name == title);
            }
        }

        public class GetBySongId : Specification<Song>
        {
            public GetBySongId(int songId)
            {
                Query.Where(x => x.Id == songId);
            }
        }
    }
}
