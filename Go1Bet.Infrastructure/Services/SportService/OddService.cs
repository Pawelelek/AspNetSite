using AutoMapper;
using Go1Bet.Core.Context;
using Go1Bet.Core.Entities.Sport;
using Go1Bet.Infrastructure.DTO_s.Sport.Bet;
using Go1Bet.Infrastructure.DTO_s.Sport.Odd;
using Go1Bet.Infrastructure.DTO_s.Sport.Opponent;
using Go1Bet.Infrastructure.DTO_s.Sport.Person;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Infrastructure.Services.SportService
{
    public class OddService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public OddService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse> GetAllAsync()
        {
            try
            {
                var categories = await _context.Odds

                    .Select(odds => new OddItemDTO
                    {
                        Id = odds.Id,
                        Name = odds.Name,
                        OpponentId = odds.OpponentId,
                        SportMatchId = odds.SportMatchId,
                        Type = odds.Type,
                        Value = odds.Value,
                        CountBets = odds.Bets.Count(),
                        BettingFund = odds.Bets.Sum(b => b.Amount)
                    }).ToListAsync();
                return new ServiceResponse
                {
                    Success = true,
                    Payload = categories
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
                var categories = await _context.Odds
                    .Where(odds => odds.Id == id)
                    .Select(odds => new OddItemDTO
                    {
                        Id = odds.Id,
                        Name = odds.Name,
                        OpponentId = odds.OpponentId,
                        SportMatchId = odds.SportMatchId,
                        Type = odds.Type,
                        Value = odds.Value,
                        Bets = odds.Bets.Where(b => b.OddId == odds.Id).Select(b => new BetItemDTO { Id = b.Id, Amount = b.Amount, BetTime = b.BetTime, OddId = b.OddId, UserId = b.UserId }).ToList(),
                        CountBets = odds.Bets.Count(),
                        BettingFund = odds.Bets.Sum(b => b.Amount)
                    }).ToListAsync();

                return new ServiceResponse
                {
                    Success = true,
                    Payload = categories
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
        public async Task<ServiceResponse> CreateAsync(OddCreateDTO model)
        {
            var odd = _mapper.Map<OddEntity>(model);
            odd.OpponentId = model.OpponentId == "" ? null : model.OpponentId;
            await _context.Odds.AddAsync(odd);
            await _context.SaveChangesAsync();
            return new ServiceResponse
            {
                Message = "Opponent was created",
                Success = true,
            };
        }
        public async Task<ServiceResponse> EditAsync(OddEditDTO model)
        {
            var oldOdds = await _context.Odds.Where(x => x.Id == model.Id).FirstOrDefaultAsync();

            if (oldOdds == null)
            {
                return new ServiceResponse()
                {
                    Message = "Upload opponent is not correct, upload is closed",
                    Success = false,
                };
            }
            var newOdds = _mapper.Map<OddEntity>(model);


            _context.Odds.Update(newOdds);
            await _context.SaveChangesAsync();

            return new ServiceResponse()
            {
                Message = "Opponent update was successful",
                Success = true,
            };

        }
        public async Task<ServiceResponse> DeleteAsync(string id)
        {
            var odds = await _context.Odds.SingleOrDefaultAsync(x => x.Id == id);
            if (odds == null)
            {
                return new ServiceResponse()
                {
                    Message = "Uploaded opponent is not correct, uploaded is closed",
                    Success = false,
                };
            }

            _context.Odds.Remove(odds);
            await _context.SaveChangesAsync();
            return new ServiceResponse()
            {
                Message = "Opponent has been deleted",
                Success = true,
            };
        }
    }
}
