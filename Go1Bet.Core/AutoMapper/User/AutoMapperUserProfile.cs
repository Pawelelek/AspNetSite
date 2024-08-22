using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Go1Bet.Core.DTO_s.User;
using Go1Bet.Core.Entities.User;
using Go1Bet.Core.DTO_s.Balance;
using Go1Bet.Core.Entities.Category;
using Go1Bet.Core.DTO_s.Category;
using Go1Bet.Core.Entities.Bonuses;
using Go1Bet.Core.DTO_s.Bonus.Exercise;
using Go1Bet.Core.DTO_s.Bonus.Promocode;

namespace Go1Bet.Core.AutoMapper.User
{
    public class AutoMapperUserProfile : Profile
    {
        public AutoMapperUserProfile()
        {
            CreateMap<CreateUserDto, AppUser>().ForMember(dst => dst.UserName, act => act.MapFrom(src => src.Email));
            CreateMap<AppUser, CreateUserDto>();
            CreateMap<AppUser, UserItemDTO>().ReverseMap();
            CreateMap<AppUser, UserEditDTO>().ReverseMap();
            CreateMap<AppUser, UserEditEmailDTO>().ReverseMap();
            CreateMap<AppUser, UserEditPasswordDTO>().ReverseMap();

            CreateMap<BalanceEntity, BalanceItemDTO>().ReverseMap();
            CreateMap<BalanceEntity, BalanceCreateDTO>().ReverseMap();

            CreateMap<TransactionEntity, TransactionItemDTO>().ReverseMap();
            CreateMap<TransactionEntity, TransactionCreateDTO>().ReverseMap();

            CreateMap<CategoryEntity, CategoryCreateDTO>().ReverseMap();
            CreateMap<CategoryEntity, CategoryItemDTO>().ReverseMap();

            CreateMap<ExerciseEntity, ExerciseItemDTO>().ReverseMap();
            CreateMap<ExerciseEntity, ExerciseCreateDTO>().ReverseMap();
            CreateMap<ExerciseEntity, ExerciseEditDTO>().ReverseMap();
            CreateMap<ExerciseEntity, ExerciseActiveDTO>().ReverseMap();

            CreateMap<PromocodeEntity, PromocodeItemDTO>().ReverseMap();
            CreateMap<PromocodeEntity, PromocodeCreateDTO>().ReverseMap();
            CreateMap<PromocodeEntity, PromocodeEditDTO>().ReverseMap();
            CreateMap<PromocodeEntity, PromocodeActiveDTO>().ReverseMap();
        }
    }
}
