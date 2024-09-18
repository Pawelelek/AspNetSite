using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Go1Bet.Core.Entities.User;
using Go1Bet.Infrastructure.DTO_s.Role;
using Microsoft.EntityFrameworkCore;
using Go1Bet.Infrastructure.DTO_s.User;

namespace Go1Bet.Infrastructure.Services
{
    public class RoleService
    {
        private readonly RoleManager<RoleEntity> _roleManager;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        public RoleService(JwtService jwtService, RoleManager<RoleEntity> roleManager, IConfiguration config, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            _mapper = mapper;
            _config = config;
            _roleManager = roleManager;
        }
        public async Task<ServiceResponse> GetAllRolesAsync()
        {
            var result = await _roleManager.Roles.Select(role => new RoleItemDTO
            {
                Id = role.Id,
                RoleName = role.Name,
                ConcurrencyStamp = role.ConcurrencyStamp
            }).ToListAsync();
            return new ServiceResponse()
            {
                Success = true,
                Message = "Get all roles",
                countPayload = result.Count,
                Payload = result
            };
        }
        public async Task<ServiceResponse> GetRoleByIdAsync(string id)
        {
            var result = await _roleManager.Roles.Where(role => role.Id == id).Select(role => new RoleItemDTO
            {
                Id = role.Id,
                RoleName = role.Name,
                ConcurrencyStamp = role.ConcurrencyStamp
            }).ToListAsync();
            if (result != null)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Message = "All roles loaded.",
                    countPayload = result.Count,
                    Payload = result
                };
            }
            return new ServiceResponse
            {
                Success = false,
                Message = "Unable to loaded roles",
            };
        }
        public async Task<ServiceResponse> CreateRoleAsync(RoleCreateDTO model)
        {
            var role = new RoleEntity { Id = Guid.NewGuid().ToString(), Name = model.RoleName };
            var result = await _roleManager.CreateAsync(role);
            return new ServiceResponse()
            {
                Success = true,
                Message = "The role has been created",
                Payload = result
            };
        }
        public async Task<ServiceResponse> EditRoleAsync(RoleEditDTO model)
        {
            var oldRole = _roleManager.FindByIdAsync(model.Id);
            var role = new RoleEntity { Id = model.Id, Name = model.RoleName };
            if(oldRole.Result.ConcurrencyStamp != null)
            {
                role.ConcurrencyStamp = model.ConcurrencyStamp;
            }
            var result = await _roleManager.UpdateAsync(role);
            return new ServiceResponse()
            {
                Success = true,
                Message = "The role has been updated",
                Payload = result
            };
        }
        public async Task<ServiceResponse> DeleteRoleByIdAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            await _roleManager.DeleteAsync(role);
            return new ServiceResponse()
            {
                Success = true,
                Message = "The role has been deleted",
                Payload = role
            };
        }
    }
}
