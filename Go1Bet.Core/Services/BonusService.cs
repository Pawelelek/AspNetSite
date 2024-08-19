using Go1Bet.Core.Context;
using Go1Bet.Core.DTO_s.Bonus;
using Go1Bet.Core.DTO_s.User;
using Go1Bet.Core.Entities.Bonuses;
using Microsoft.EntityFrameworkCore;

namespace Go1Bet.Core.Services
{
    public class BonusService
    {
        private readonly AppDbContext _context;
        public BonusService(AppDbContext context) 
        { 
           _context = context;
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
        public async Task<ServiceResponse> ActivePromocodeAsync(string userId, string key)
        {
            var promocodes = _context.Promocodes.ToList();
            foreach(var promo in promocodes)
            {
                if(promo.Key == key)
                {
                    if(promo.CountAvailable <= promo.CountEntries)
                    {
                        return new ServiceResponse
                        {
                            Message = "Error! The number of activations is limited!",
                            Success = false,
                        };
                    }
                    var entity = new PromocodeUserEntity() { DateCreated = DateTime.Now, UserId = userId, PromocodeId = promo.Id };
                    promo.CountEntries++;
                    _context.Promocodes.Update(promo);
                    await _context.SaveChangesAsync();
                    await _context.UserPromocodes.AddAsync(entity);
                    await _context.SaveChangesAsync();
                }
            }
            return new ServiceResponse
            {
                Message = "Promocode has been created.",
                Success = true,
            };
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
    }
}
