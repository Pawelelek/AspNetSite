using AutoMapper;
using Go1Bet.Core.Context;
using Go1Bet.Infrastructure.DTO_s.Bonus;
using Go1Bet.Infrastructure.DTO_s.Bonus.Promocode;
using Go1Bet.Infrastructure.DTO_s.User;
using Go1Bet.Core.Entities.Bonuses;
using Go1Bet.Core.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Go1Bet.Infrastructure.Services
{
    public class BonusService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly BalanceService _balanceService;
        
        public BonusService(AppDbContext context, IMapper mapper, UserManager<AppUser> userManager, BalanceService balanceService) 
        {
            _mapper = mapper;
           _context = context;
            _userManager = userManager;
            _balanceService = balanceService;
        }
        public async Task<ServiceResponse> GetAllPromocodesAsync()
        {
            try
            {
                var balances = await _context.Promocodes
                    .Select(p => new PromocodeItemDTO
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Key = p.Key,
                        CountAvailable = p.CountAvailable,
                        CountEntries = p.CountEntries,
                        DateCreated = p.DateCreated.ToString(),
                        ExpirationDate = p.ExpirationDate.ToString(),
                        PriceMoney = p.PriceMoney,
                    }).ToListAsync();

                return new ServiceResponse
                {
                    Success = true,
                    Payload = balances
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<ServiceResponse> GetPromocodeByIdAsync(string id)
        {
            try
            {
                var balances = await _context.Promocodes
                    .Where(p => p.Id == id)
                    .Select(p => new PromocodeItemDTO
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Key = p.Key,
                        CountAvailable = p.CountAvailable,
                        CountEntries = p.CountEntries,
                        DateCreated = p.DateCreated.ToString(),
                        ExpirationDate = p.ExpirationDate.ToString(),
                        PriceMoney = p.PriceMoney,
                        Users = p.PromocodeUsers.Select(u => new UserItemDTO { Id = u.User.Id, FirstName = u.User.FirstName }).ToList()
                    }).ToListAsync();

                return new ServiceResponse
                {
                    Success = true,
                    Payload = balances
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<ServiceResponse> ActivePromocodeAsync(PromocodeActiveDTO model)
        {
            var promo = await _context.Promocodes.Where(p => p.Key == model.Key).FirstOrDefaultAsync();
            var userPromoValid = _context.UserPromocodes.Where(up => up.UserId == model.UserId && up.PromocodeId == promo.Id).Any();
            if(userPromoValid)
            {
                return new ServiceResponse
                {
                    Message = "The promo code has already been activated!",
                    Success = false,
                };
            }
            if (promo.CountAvailable <= promo.CountEntries)
            {
                return new ServiceResponse
                {
                    Message = "Error! The number of activations is limited!",
                    Success = false,
                };
            }
            var entity = new PromocodeUserEntity() { DateCreated = DateTime.Now, UserId = model.UserId, PromocodeId = promo.Id };
            promo.CountEntries++;
            _context.Promocodes.Update(promo);
            
            var user = await _userManager.FindByIdAsync(model.UserId);
            _balanceService.BalanceInteraction(user.SwitchedBalanceId, promo.PriceMoney, $"Promo - {promo.Name}");
            //var balance = await _context.Balances.Where(b => b.Id == user.SwitchedBalanceId).FirstOrDefaultAsync();
            //balance.Money += promo.PriceMoney;
            //_context.Balances.Update(balance);

            //await _context.UserPromocodes.AddAsync(entity);
            //await _context.SaveChangesAsync();
            return new ServiceResponse
            {
                Message = "Promocode has been activated.",
                Success = true,
            };
        }
        public async Task<ServiceResponse> CreatePromocodeAsync(PromocodeCreateDTO model)
        {
            var promocode = new PromocodeEntity() 
            { PriceMoney = model.PriceMoney, CountAvailable = model.CountAvailibale, DateCreated = DateTime.UtcNow, ExpirationDate = model.ExpirationDate, Key = model.Key, Name = model.Name };

            await _context.Promocodes.AddAsync(promocode);
            await _context.SaveChangesAsync();
            return new ServiceResponse
            {
                Message = "Promocode has been created.",
                Success = true,
            };
        }
        public async Task<ServiceResponse> EditPromocodeAsync(PromocodeEditDTO model)
        {
            //var promocode = new PromocodeEntity()
            //{ PriceMoney = model.PriceMoney, CountAvailable = model.CountAvailable, ExpirationDate = model.ExpirationDate, Key = model.Key, Name = model.Name };
            var oldPromo = _context.Promocodes.Where(p => p.Id == model.Id).FirstOrDefault();
            var newPromo = _mapper.Map(model, oldPromo);
            _context.Promocodes.Update(newPromo);
            await _context.SaveChangesAsync();
            return new ServiceResponse
            {
                Message = "Promocode has been updated.",
                Success = true,
            };
        }
        public async Task<ServiceResponse> DeletePromoByIdAsync(string id)
        {
            var promo = await _context.Promocodes.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (promo == default)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Promo was not found."
                };
            }
            foreach (var item in await _context.UserPromocodes.Where(up => up.PromocodeId == id ).ToListAsync())
            {
                _context.UserPromocodes.Remove(item);
            }
            var result = _context.Promocodes.Remove(promo);
            await _context.SaveChangesAsync();
            return new ServiceResponse
            {
                Success = true,
                Message = "Promo successfull deleted."
            };
        }
    }
}
