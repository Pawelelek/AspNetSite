using AutoMapper;
using Go1Bet.Core.Constants;
using Go1Bet.Core.Context;
using Go1Bet.Core.Entities.Sport;
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
    public class OpponentService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public OpponentService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse> GetAllAsync()
        {
            try
            {
                var categories = await _context.Opponents

                    .Select(o => new OpponentItemDTO
                    {
                        Id = o.Id,
                        Name = o.Name,
                        DateCreated = o.DateCreated,
                        SportMatchName = o.SportMatch.Name,
                        SportMatchId = o.SportMatchId,
                        countTeammates = o.Teammates.Where(t => t.OpponentId == o.Id).Count(),
                        Score = o.Score,
                        CountryCode = o.CountryCode,
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
                var categories = await _context.Opponents
                    .Where(o => o.Id == id)
                    .Select(o => new OpponentItemDTO
                    {
                        Id = o.Id,
                        Name = o.Name,
                        DateCreated = o.DateCreated,
                        SportMatchName = o.SportMatch.Name,
                        SportMatchId = o.SportMatchId,
                        Teammates = o.Teammates.Where(t => t.OpponentId == o.Id).Select(x => new PersonItemDTO { Id = x.Id, Name = x.Name, OpponentId = x.OpponentId, OpponentName = x.Opponent.Name, Position = x.Position }).ToList(),
                        countTeammates = o.Teammates.Where(t => t.OpponentId == o.Id).Count(),
                        Score = o.Score,
                        CountryCode = o.CountryCode,
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
        public async Task<ServiceResponse> CreateAsync(OpponentCreateDTO model)
        {
            var person = _mapper.Map<OpponentEntity>(model);
            await _context.Opponents.AddAsync(person);
            await _context.SaveChangesAsync();
            return new ServiceResponse
            {
                Message = "Opponent was created",
                Success = true,
            };
        }
        public async Task<ServiceResponse> EditAsync(OpponentEditDTO model)
        {
            var oldOpponent = await _context.Opponents.Where(x => x.Id == model.Id).FirstOrDefaultAsync();

            if (oldOpponent == null)
            {
                return new ServiceResponse()
                {
                    Message = "Upload opponent is not correct, upload is closed",
                    Success = false,
                };
            }
            var newOpponent = _mapper.Map<OpponentEntity>(model);
            

            _context.Opponents.Update(newOpponent);
            await _context.SaveChangesAsync();

            return new ServiceResponse()
            {
                Message = "Opponent update was successful",
                Success = true,
            };

        }
        public async Task<ServiceResponse> DeleteAsync(string id)
        {
            var opponent = await _context.Opponents.SingleOrDefaultAsync(x => x.Id == id);
            if (opponent == null)
            {
                return new ServiceResponse()
                {
                    Message = "Uploaded opponent is not correct, uploaded is closed",
                    Success = false,
                };
            }



            _context.Opponents.Remove(opponent);
            await _context.SaveChangesAsync();
            return new ServiceResponse()
            {
                Message = "Opponent has been deleted",
                Success = true,
            };
        }
    }
}
