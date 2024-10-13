using AutoMapper;
using Go1Bet.Core.Context;
using Go1Bet.Core.Entities.Category;
using Go1Bet.Core.Entities.Sport;
using Go1Bet.Infrastructure.DTO_s.Category;
using Go1Bet.Infrastructure.DTO_s.Sport.Person;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Infrastructure.Services.SportService
{
    public class PersonService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public PersonService(AppDbContext context, IMapper mapper)
        {
            _context = context; 
            _mapper = mapper;
        }
        public async Task<ServiceResponse> GetAllAsync()
        {
            try
            {
                var categories = await _context.Persons

                    .Select(p => new PersonItemDTO
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Position = p.Position,
                        OpponentId = p.OpponentId,
                        OpponentName = p.Opponent.Name,
                        Number = p.Number,
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
                var categories = await _context.Persons
                    .Where(p => p.Id == id)
                    .Select(p => new PersonItemDTO
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Position = p.Position,
                        OpponentId = p.OpponentId,
                        OpponentName = p.Opponent.Name,
                        Number = p.Number,
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
        public async Task<ServiceResponse> CreateAsync(PersonCreateDTO model)
        {
            var person = _mapper.Map<PersonEntity>(model);
            person.OpponentId = model.OpponentId == "string" ? null : model.OpponentId;
            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();
            return new ServiceResponse
            {
                Message = "Person was created",
                Success = true,
            };
        }
        public async Task<ServiceResponse> EditAsync(PersonEditDTO model)
        {
            var person = await _context.Persons.Where(x => x.Id == model.Id).FirstOrDefaultAsync();

            if (person == null)
            {
                return new ServiceResponse()
                {
                    Message = "Upload person is not correct, upload is closed",
                    Success = false,
                };
            }

            person.Name = model.Name;
            person.OpponentId = model.OpponentId == "string" ? null : model.OpponentId;
            person.Position = model.Position;

            _context.Persons.Update(person);
            await _context.SaveChangesAsync();

            return new ServiceResponse()
            {
                Message = "Person update was successful",
                Success = true,
            };

        }
        public async Task<ServiceResponse> DeleteAsync(string id)
        {
            var person = await _context.Persons.SingleOrDefaultAsync(x => x.Id == id);
            if (person == null)
            {
                return new ServiceResponse()
                {
                    Message = "Uploaded person is not correct, uploaded is closed",
                    Success = false,
                };
            }



            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            return new ServiceResponse()
            {
                Message = "Person has been deleted",
                Success = true,
            };
        }
    }
}
