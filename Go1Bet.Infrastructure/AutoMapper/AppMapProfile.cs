using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Go1Bet.Infrastructure.DTO_s.User;
using Go1Bet.Core.Entities.User;
using Go1Bet.Infrastructure.DTO_s.Balance;
using Go1Bet.Core.Entities.Category;
using Go1Bet.Infrastructure.DTO_s.Category;
using Go1Bet.Core.Entities.Bonuses;
using Go1Bet.Infrastructure.DTO_s.Bonus.Promocode;
using Go1Bet.Core.Entities.Sport;
using Go1Bet.Infrastructure.DTO_s.Sport.Person;
using Go1Bet.Infrastructure.DTO_s.Sport.Opponent;

namespace Go1Bet.Infrastructure.AutoMapper
{
    public class AppMapProfile : Profile
    {
        public AppMapProfile()
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

            CreateMap<PromocodeEntity, PromocodeItemDTO>().ReverseMap();
            CreateMap<PromocodeEntity, PromocodeCreateDTO>().ReverseMap();
            CreateMap<PromocodeEntity, PromocodeEditDTO>().ReverseMap();
            CreateMap<PromocodeEntity, PromocodeActiveDTO>().ReverseMap();

            CreateMap<PersonEntity, PersonItemDTO>().ReverseMap();
            CreateMap<PersonEntity, PersonCreateDTO>().ReverseMap();
            CreateMap<PersonEntity, PersonEditDTO>().ReverseMap();

            CreateMap<OpponentEntity, OpponentItemDTO>().ReverseMap();
            CreateMap<OpponentEntity, OpponentCreateDTO>().ReverseMap();
            CreateMap<OpponentEntity, OpponentEditDTO>().ReverseMap();
        }
    }
}
