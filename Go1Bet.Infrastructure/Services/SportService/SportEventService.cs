using AutoMapper;
using Go1Bet.Core.Context;
using Go1Bet.Core.Entities.Sport;
using Go1Bet.Infrastructure.DTO_s.Sport.Person;
using Go1Bet.Infrastructure.DTO_s.Sport.SportEvent;
using Go1Bet.Infrastructure.DTO_s.Sport.SportMatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Infrastructure.Services.SportService
{
    public class SportEventService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public SportEventService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse> GetAllAsync()
        {
            try
            {
                var sportEvents = await _context.SportEvents

                    .Select(se => new SportEventItemDTO
                    {
                        Id = se.Id,
                        Name = se.Name,
                        DateCreated = se.DateCreated,
                        DateStart = se.DateStart,
                        DateEnd = se.DateEnd,
                        Description = se.Description,
                        Status = se.Status,
                        ParentId = se.ParentId,
                        ParentName = se.Parent.Name,
                        countSportMatches = se.SportMatches.Count()
                    }).ToListAsync();

                return new ServiceResponse
                {
                    Success = true,
                    Payload = sportEvents
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
                var sportEvents = await _context.SportEvents
                    .Where(p => p.Id == id)
                    .Select(se => new SportEventItemDTO
                    {
                        Id = se.Id,
                        Name = se.Name,
                        DateCreated = se.DateCreated,
                        DateStart = se.DateStart,
                        DateEnd = se.DateEnd,
                        Description = se.Description,
                        countSportMatches = se.SportMatches.Count(),
                        Status = se.Status,
                        ParentId = se.ParentId,
                        ParentName = se.Parent.Name,
                        SportMatches = se.SportMatches.Where(sm => sm.SportEventId == id)
                        .Select(x => new SportMatchItemDTO { 
                            Id = x.Id, Name = x.Name, DateCreated = x.DateCreated, DateEnd = x.DateEnd, DateStart = x.DateStart, 
                            CountBets = x.Odds.Sum(x => x.Bets.Count()), BettingFund = x.Odds.Sum(x=> x.Bets.Sum(b => b.Amount)), SportEventId = x.SportEventId
                        }).ToList(),
                    }).ToListAsync();

                return new ServiceResponse
                {
                    Success = true,
                    Payload = sportEvents
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
        public async Task<ServiceResponse> CreateAsync(SportEventCreateDTO model)
        {
            var sportEvent = _mapper.Map<SportEventEntity>(model);
            sportEvent.ParentId = model.ParentId == "string" || model.ParentId == null ? null : model.ParentId;
            await _context.SportEvents.AddAsync(sportEvent);
            await _context.SaveChangesAsync();
            return new ServiceResponse
            {
                Message = "SportEvent was created",
                Success = true,
            };
        }
        public async Task<ServiceResponse> EditAsync(SportEventEditDTO model)
        {
            var oldSportEvent = await _context.SportEvents.Where(x => x.Id == model.Id).FirstOrDefaultAsync();

            if (oldSportEvent == null)
            {
                return new ServiceResponse()
                {
                    Message = "Upload sport event is not correct, upload is closed",
                    Success = false,
                };
            }
            var newSportEvent = _mapper.Map<SportEventEntity>(model);
            newSportEvent.ParentId = model.ParentId == "string" || model.ParentId == null ? null : model.ParentId;
            _context.SportEvents.Update(newSportEvent);
            await _context.SaveChangesAsync();

            return new ServiceResponse()
            {
                Message = "Sport Event update was successful",
                Success = true,
            };

        }
        public async Task<ServiceResponse> DeleteAsync(string id)
        {
            var person = await _context.SportEvents.SingleOrDefaultAsync(x => x.Id == id);
            if (person == null)
            {
                return new ServiceResponse()
                {
                    Message = "Uploaded sport event is not correct, uploaded is closed",
                    Success = false,
                };
            }



            _context.SportEvents.Remove(person);
            await _context.SaveChangesAsync();
            return new ServiceResponse()
            {
                Message = "Sport Event has been deleted",
                Success = true,
            };
        }
    }
}
