using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNewsApi.Core.DTO_s.User;
using TopNewsApi.Core.Entities.User;

namespace TopNewsApi.Core.AutoMapper.Songs
{
    public class AutoMapperSongUser : Profile
    {
        public AutoMapperSongUser()
        {
            CreateMap<SongUserDto, SongUser>().ReverseMap();
        }
    }
}
