using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNewsApi.Core.Interfaces;

namespace TopNewsApi.Core.Entities.User
{
    public class SongUser: IEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int SongId { get; set; }
        public AppUser User { get; set; }
        public Song Song { get; set; }
    }
}
