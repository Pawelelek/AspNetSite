using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Go1Bet.Core.DTO_s.Token;
using Go1Bet.Core.DTO_s.User;
using Go1Bet.Core.Entities.Specifications;
using Go1Bet.Core.Entities.Tokens;
using Go1Bet.Core.Entities.User;
using Go1Bet.Core.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Go1Bet.Core.Constants;
using Google.Apis.Auth;
using Org.BouncyCastle.Bcpg;

namespace Go1Bet.Core.Services
{
    public class UserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<RoleEntity> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly JwtService _jwtService;
        public UserService(JwtService jwtService, RoleManager<RoleEntity> roleManager, IConfiguration config, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _config = config;
            _roleManager = roleManager;
            _jwtService = jwtService;
        }

        public async Task<ServiceResponse> CreateAsync(CreateUserDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                return new ServiceResponse
                {
                    Message = "User exists.",
                    Success = false,
                };
            }

            var mappedUser = _mapper.Map<CreateUserDto, AppUser>(model);
            mappedUser.Id = Guid.NewGuid().ToString();
            IdentityResult result = await _userManager.CreateAsync(mappedUser, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(mappedUser, model.Role);

                return new ServiceResponse
                {
                    Message = "User successfully created.",
                    Success = true,
                };
            }

            List<IdentityError> errorList = result.Errors.ToList();

            string errors = "";
            foreach (var error in errorList)
            {
                errors = errors + error.Description.ToString();
            }

            return new ServiceResponse
            {
                Message = "User creating error.",
                Success = false,
                Payload = errors
            };

        }
        //1. UPDATE > Name, LastName, PhoneNumber
        //2. UPDATE > Password
        public async Task<ServiceResponse> GetPasswordResetTokenAsync (string userId)
        {
            var existUser = await _userManager.FindByIdAsync(userId);
            if(existUser == null)
            {
                return new ServiceResponse
                {
                    Message = "User not found",
                    Success = false
                };
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(existUser);
            return new ServiceResponse
            {
                Success = true,
                Payload = token,
                Message = "Success"
            };
        }
        public async Task<ServiceResponse> UpdateUserPasswordAsync(UserEditPasswordDTO model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                return new ServiceResponse
                {
                    Message = "Passwords do not match",
                    Success = false,
                    Payload = null,
                };
            }
            var oldUser = await _userManager.FindByIdAsync(model.Id);
            //var token = await _userManager.GeneratePasswordResetTokenAsync(oldUser);
            await _userManager.ResetPasswordAsync(oldUser, model.Token, model.ConfirmPassword);
            oldUser.DateLastPasswordUpdated = DateTime.UtcNow;
            var result = await _userManager.UpdateAsync(oldUser);
            return new ServiceResponse
            {
                Message = "Password has been updated",
                Success = true,
                Payload = result,
            };
        }
        public async Task<ServiceResponse> UpdateUserEmailAsync(UserEditEmailDTO model)
        {
            var existUser = await _userManager.FindByEmailAsync(model.Email);
            if (existUser != null)
            {
                return new ServiceResponse
                {
                    Message = "A user with this email address already exists",
                    Success = false,
                    Payload = null,
                };
            }
            var oldUser = await _userManager.FindByIdAsync(model.Id);

            var newUser = _mapper.Map(model, oldUser);
            newUser.DateLastEmailUpdated = DateTime.UtcNow;
            var result = await _userManager.UpdateAsync(newUser);
            return new ServiceResponse
            {
                Message = "Email has been updated",
                Success = true,
                Payload = result,
            };
        }
        public async Task<ServiceResponse> UpdateUserPersonalInfoAsync(UserEditDTO model)
        {
            var oldUser = await _userManager.FindByIdAsync(model.Id.ToString());
            var user = _mapper.Map(model, oldUser);
            if (user != null)
            {
                user.DateLastPersonalInfoUpdated = DateTime.UtcNow;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return new ServiceResponse
                    {
                        Message = "User updated",
                        Success = true,
                        Payload = result,
                        Errors = result.Errors
                    };
                }
                return new ServiceResponse
                {
                    Message = "Error occured",
                    Success = false,
                    Payload = result,
                    Errors = result.Errors
                };
            }
            return new ServiceResponse
            {
                Message = "User not found",
                Success = false,
                Payload = null,
            };
        }
        public async Task<ServiceResponse> UpdateUserRoleAsync(UserEditRoleDTO model)
        {
            var existUser = await _userManager.FindByIdAsync(model.UserId.ToString());
            if(existUser != null)
            {
                if (model.RoleName != null)
                {

                    var oldRoles = await _userManager.GetRolesAsync(existUser);
                    await _userManager.RemoveFromRolesAsync(existUser, oldRoles);

                    var role = model.RoleName != null ? model.RoleName : Roles.User;
                    var result = await _userManager.AddToRoleAsync(existUser, role);
                    return new ServiceResponse
                    {
                        Message = "Success",
                        Success = true,
                        Payload = result,
                    };
                }
            }
            return new ServiceResponse
            {
                Message = "User not found // model.RoleName is empty",
                Success = false,
                Payload = null,
            };
        }

        public async Task<ServiceResponse> GetAllAsync()
        {

            var result = await _userManager.Users
                    .Select(user => new UserItemDTO
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        EmailConfirmed = user.EmailConfirmed,
                        DateLastEmailUpdated = user.DateLastEmailUpdated.ToString(),
                        DateCreated = user.DateCreated.ToString(),
                        DateLastPasswordUpdated = user.DateLastPasswordUpdated.ToString(),
                        DateLastPersonalInfoUpdated = user.DateLastPersonalInfoUpdated.ToString(),
                        LockedEnd = user.LockoutEnd.ToString(),
                        Roles = user.UserRoles.Select(perm => new UserRoleItemDTO { RoleName = perm.Role.Name }).ToList()
                        //Role = _userManager.GetRolesAsync(user) //Working
                        // Role = _userManager.GetRolesAsync(user).ToString().First().ToString(),
                    }).ToListAsync();

            return new ServiceResponse
            {
                Success = true,
                Message = "All users loaded.",
                countPayload = result.Count,
                Payload = result
            };
        }

        public async Task<ServiceResponse> GetByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                //var updatedUser = _mapper.Map(user, new UserItemDTO());
                var result = await _userManager.Users.Where(user => user.Id == id)
                    .Select(user => new UserItemDTO
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    EmailConfirmed = user.EmailConfirmed,
                    DateLastEmailUpdated = user.DateLastEmailUpdated.ToString(),
                    DateCreated = user.DateCreated.ToString(),
                    DateLastPasswordUpdated = user.DateLastPasswordUpdated.ToString(),
                    DateLastPersonalInfoUpdated = user.DateLastPersonalInfoUpdated.ToString(),
                    LockedEnd = user.LockoutEnd.ToString(),
                    Roles = user.UserRoles.Select(perm => new UserRoleItemDTO { RoleName = perm.Role.Name }).ToList()
                    }).ToListAsync();

                return new ServiceResponse
                {
                    Message = "Success",
                    Success = true,
                    countPayload = result.Count,
                    Payload = result
                };
            }

            return new ServiceResponse
            {
                Message = "User not found",
                Success = false,
                Payload = null
            };
        }

        public async Task<ServiceResponse> LoginUserAsync(LoginUserDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new ServiceResponse
                {
                    Message = "Login or password incorrect.",
                    Success = false
                };
            }

            var signinResult = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: true, lockoutOnFailure: true);
            if (signinResult.Succeeded)
            {
                var tokens = await _jwtService.GenerateJwtTokensAsync(user);
                return new ServiceResponse
                {
                    AccessToken = tokens.Token,
                    RefreshToken = tokens.refreshToken.Token,
                    Message = "User signed in successfully.",
                    Success = true,
                };
            }

            if (signinResult.IsNotAllowed)
            {
                return new ServiceResponse
                {
                    Message = "Confirm your email please.",
                    Success = false
                };
            }

            if (signinResult.IsLockedOut)
            {
                return new ServiceResponse
                {
                    Message = "User is blocked connect to support.",
                    Success = false
                };
            }

            return new ServiceResponse
            {
                Message = "Login or password incorrect.",
                Success = false
            };
        }

        public async Task<ServiceResponse> LogoutUserAsync(string userId)
        {
            var result = await _jwtService.GetById(userId);
            if (result != null)
            {
                foreach (var item in result)
                {
                    await _jwtService.Delete(item);
                }
            }
            await _signInManager.SignOutAsync();
            return new ServiceResponse
            {
                Message = "User successfully logged out",
                Success = true
            };
            
        }

        public async Task<ServiceResponse> RefreshTokenAsync(TokenRequestDto model)
        {
            return await _jwtService.VerifyTokenAsync(model);
        }

        public async Task<ServiceResponse> DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "User not found."
                };
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Message = "User successfullt deleted."
                };
            }

            return new ServiceResponse
            {
                Success = false,
                Message = "Something wrong. Connect with your admin.",
                Errors = result.Errors.Select(e => e.Description)
            };
        }
        public async Task<ServiceResponse> GoogleExternalLogin(GoogleExternalLoginDTO model)
        {
            //Install packet Google.Apis.Auth
            //So that the backend checks whether the user is authorized through Google
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>()
                {
                    //"85911906235-mpbk79c4do3jhbf2drgemm9q2n2sd6ca.apps.googleusercontent.com",
                    Encoding.ASCII.GetBytes(_config["GoogleID:Secret"]).ToString()
                }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(model.Token, settings);
            if (payload != null)
            {
                var info = new UserLoginInfo(model.Provider, payload.Subject, model.Provider);
                var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
                //var user = await _userRepository.GetUserByLoginAsync(info.LoginProvider, info.ProviderKey);
                if (user == null)
                {
                    user = await _userManager.FindByEmailAsync(payload.Email);
                    if (user == null)
                    {
                        user = new AppUser()
                        {
                            Email = payload.Email,
                            UserName = payload.Email,
                            FirstName = payload.GivenName,
                            LastName = payload.FamilyName,
                            //Image = payload.Picture,
                            IsGoogle = true
                        };
                        user.Id = Guid.NewGuid().ToString();
                        var resultCreate = await _userManager.CreateAsync(user);
                        if (!resultCreate.Succeeded)
                        {
                            return new ServiceResponse
                            {
                                Success = false,
                                Message = "Something went wrong"
                            };
                        }
                        if (user.IsGoogle == false)
                        {
                            await _userManager.AddToRoleAsync(user, Roles.User);
                        }
                    }
                    var resultAddLogin = await _userManager.AddLoginAsync(user, info);
                    if (!resultAddLogin.Succeeded)
                    {
                        return new ServiceResponse
                        {
                            Success = false,
                            Message = "Something went wrong"
                        };
                    }

                }

                var result = new { Id = user.Id, email = user.Email, firstname = user.FirstName, lastname = user.LastName, phoneNumber = user.PhoneNumber };
                var token = await _jwtService.GenerateJwtTokensAsync(user);
                return new ServiceResponse
                {
                    Success = true,
                    Payload = token
                };
            }
            return new ServiceResponse
            {
                Success = false,
                Message = "Something went wrong"
            };

        }

    }
}
