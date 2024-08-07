﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Go1Bet.Core.Entities.User;
using Go1Bet.Core.Interfaces;
using Go1Bet.Core.DTO_s.Role;
using Microsoft.EntityFrameworkCore;
using Go1Bet.Core.DTO_s.User;

namespace Go1Bet.Core.Services
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
            var result = _roleManager.Roles.ToListAsync().Result.Select(role => new RoleItemDTO
            {
                Id = role.Id,
                RoleName = role.Name,
                ConcurrencyStamp = role.ConcurrencyStamp
            }).ToList();
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
            var result = _roleManager.Roles.ToListAsync().Result.Where(role => role.Id == id).Select(role => new RoleItemDTO
            {
                Id = role.Id,
                RoleName = role.Name,
                ConcurrencyStamp = role.ConcurrencyStamp
            }).ToList();
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
            var role = new RoleEntity { Id = model.Id, Name = model.RoleName, ConcurrencyStamp = model.ConcurrencyStamp };
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
