using AutoMapper;
using Go1Bet.Core.Context;
using Go1Bet.Core.Entities.Sport;
using Go1Bet.Infrastructure.DTO_s.Sport.Bet;
using Go1Bet.Infrastructure.DTO_s.Sport.Odd;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Infrastructure.Services.SportService
{
    public class BetService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public BetService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse> GetAllAsync()
        {
            try
            {
                var bets = await _context.Bets

                    .Select(bet => new BetItemDTO
                    {
                        Id = bet.Id,
                        Amount = bet.Amount,
                        Value = bet.Odd.Value,
                        BetTime = bet.BetTime.ToString("yyyy-MM-dd HH:mm"),
                        OddId = bet.OddId,
                        OddName = bet.Odd.Name,
                        UserId = bet.UserId,
                        UserName = bet.User.FirstName
                    }).ToListAsync();
                return new ServiceResponse
                {
                    Success = true,
                    Payload = bets
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
        public async Task<ServiceResponse> GetByIdAsync(string id)
        {
            try
            {
                var bets = await _context.Bets
                    .Where(bet => bet.Id == id)
                    .Select(bet => new BetItemDTO
                    {
                        Id = bet.Id,
                        Amount = bet.Amount,
                        Value = bet.Odd.Value,
                        BetTime = bet.BetTime.ToString("yyyy-MM-dd HH:mm"),
                        OddId = bet.OddId,
                        OddName = bet.Odd.Name,
                        UserId = bet.UserId,
                        UserName = bet.User.FirstName
                    }).ToListAsync();

                return new ServiceResponse
                {
                    Success = true,
                    Payload = bets
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
        public async Task<ServiceResponse> CreateAsync(BetCreateDTO model)
        {
            var bet = _mapper.Map<BetEntity>(model);
            var balance = await _context.Balances.Where(b => b.UserId == model.UserId).FirstOrDefaultAsync();
            if(model.Amount > balance.Money)
            {
                return new ServiceResponse
                {
                    Message = "Dont have enought money.",
                    Success = true,
                };
            }
               
            
            await _context.Bets.AddAsync(bet);
            await _context.SaveChangesAsync();
            var value = new Random().Next(0, 4);
            var odd = await _context.Odds.Where(o => o.Id == model.OddId).FirstOrDefaultAsync();
            if (value > 2)
            {
                var money = model.Amount * (double)odd.Value;
                balance.Money += money;
                _context.Balances.Update(balance);
                await _context.SaveChangesAsync();
                return new ServiceResponse
                {
                    Message = $"Ви виграли {money}",
                    Success = true,
                };
            }
            var betExist = await _context.Bets.Where(ba => ba.UserId == model.UserId).AnyAsync();
            if (betExist)
            {
                balance.Money -= model.Amount;
                _context.Balances.Update(balance);
                await _context.SaveChangesAsync();
            }
            return new ServiceResponse
            {
                Message = "Ви програли ставку",
                Success = true,
            };
        }

        public async Task<ServiceResponse> EditAsync(BetEditDTO model)
        {
            var oldBet = await _context.Bets.Where(x => x.Id == model.Id).FirstOrDefaultAsync();

            if (oldBet == null)
            {
                return new ServiceResponse()
                {
                    Message = "Upload opponent is not correct, upload is closed",
                    Success = false,
                };
            }
            var newBet = _mapper.Map<BetEntity>(model);


            _context.Bets.Update(newBet);
            await _context.SaveChangesAsync();

            return new ServiceResponse()
            {
                Message = "Opponent update was successful",
                Success = true,
            };

        }
        public async Task<ServiceResponse> DeleteAsync(string id)
        {
            var bet = await _context.Bets.SingleOrDefaultAsync(x => x.Id == id);
            if (bet == null)
            {
                return new ServiceResponse()
                {
                    Message = "Uploaded opponent is not correct, uploaded is closed",
                    Success = false,
                };
            }

            _context.Bets.Remove(bet);
            await _context.SaveChangesAsync();
            return new ServiceResponse()
            {
                Message = "Opponent has been deleted",
                Success = true,
            };
        }
    }
}
