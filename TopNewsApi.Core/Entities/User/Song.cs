using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNewsApi.Core.Interfaces;

namespace TopNewsApi.Core.Entities.User
{
    public class Song: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SongUrl { get; set; }
        List<IEnumerable<SongUser>> SongList { get; set; }
    }
}
