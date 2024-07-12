using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Go1Bet.Core.DTO_s.User;
using Go1Bet.Core.Entities.User;

namespace Go1Bet.Core.AutoMapper.User
{
    public class AutoMapperUserProfile : Profile
    {
        public AutoMapperUserProfile()
        {
            CreateMap<UserItemDTO, AppUser>().ReverseMap();
            CreateMap<UserEditDTO, AppUser>().ReverseMap();
            CreateMap<CreateUserDto, AppUser>().ForMember(dst => dst.UserName, act => act.MapFrom(src => src.Email));
            CreateMap<AppUser, CreateUserDto>();
            CreateMap<AppUser, UserItemDTO>().ReverseMap();
            CreateMap<AppUser, UserEditEmailDTO>().ReverseMap();
            CreateMap<AppUser, UserEditPasswordDTO>().ReverseMap();
        }
    }
}
