using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Infrastructure.Services.SportService
{
    using AutoMapper;
    using global::AutoMapper;
    using global::Go1Bet.Core.Context;
    using global::Go1Bet.Core.Entities.Sport;
    using global::Go1Bet.Infrastructure.DTO_s.Sport.Bet;
    using global::Go1Bet.Infrastructure.DTO_s.Sport.Country;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace Go1Bet.Infrastructure.Services.SportService
    {
        public class CountryService
        {
            private readonly IMapper _mapper;
            private readonly AppDbContext _context;
            public CountryService(AppDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<ServiceResponse> GetAllAsync()
            {
                try
                {
                    var countries = await _context.Countries

                        .Select(c => new CountryItemDTO
                        {
                            Id = c.Id,
                            Name = c.Name,
                        }).ToListAsync();
                    return new ServiceResponse
                    {
                        Success = true,
                        Payload = countries
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
                    var countries = await _context.Countries
                        .Where(bet => bet.Id == id)
                        .Select(c => new CountryItemDTO
                        {
                            Id = c.Id,
                            Name = c.Name,
                        }).ToListAsync();

                    return new ServiceResponse
                    {
                        Success = true,
                        Payload = countries
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
            public async Task<ServiceResponse> CreateAsync(CountryCreateDTO model)
            {
                var country = _mapper.Map<CountryEntity>(model);
                await _context.Countries.AddAsync(country);
                await _context.SaveChangesAsync();
                return new ServiceResponse
                {
                    Message = "Country was created",
                    Success = true,
                };
            }
            public async Task<ServiceResponse> EditAsync(CountryEditDTO model)
            {
                var oldCountry = await _context.Countries.Where(x => x.Id == model.Id).FirstOrDefaultAsync();

                if (oldCountry == null)
                {
                    return new ServiceResponse()
                    {
                        Message = "Upload country is not correct, upload is closed",
                        Success = false,
                    };
                }
                var newCountry = _mapper.Map<CountryEntity>(model);


                _context.Countries.Update(newCountry);
                await _context.SaveChangesAsync();

                return new ServiceResponse()
                {
                    Message = "Country update was successful",
                    Success = true,
                };

            }
            public async Task<ServiceResponse> DeleteAsync(string id)
            {
                var country = await _context.Countries.SingleOrDefaultAsync(x => x.Id == id);
                if (country == null)
                {
                    return new ServiceResponse()
                    {
                        Message = "Uploaded country is not correct, uploaded is closed",
                        Success = false,
                    };
                }

                _context.Countries.Remove(country);
                await _context.SaveChangesAsync();
                return new ServiceResponse()
                {
                    Message = "Country has been deleted",
                    Success = true,
                };
            }
        }
    }

}
