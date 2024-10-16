﻿using AutoMapper;
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
using Go1Bet.Infrastructure.DTO_s.Token;
using Go1Bet.Infrastructure.DTO_s.User;
using Microsoft.AspNetCore.Http;
using Go1Bet.Core.Entities.User;
using Go1Bet.Core.Constants;
using Google.Apis.Auth;
using Org.BouncyCastle.Bcpg;
using Microsoft.AspNetCore.WebUtilities;
using MailKit.Security;
using MimeKit;
using Go1Bet.Core.Context;
using Go1Bet.Infrastructure.DTO_s.Bonus.Promocode;
using Microsoft.AspNetCore.Mvc;
using Go1Bet.Infrastructure.DTO_s.User.ForgetPassword;
using Go1Bet.Infrastructure.DTO_s.Sport.FavouriteSportMatch;
using Go1Bet.Infrastructure.DTO_s.Sport.Bet;
using Go1Bet.Infrastructure.DTO_s.Sport.Odd;
using Go1Bet.Infrastructure.DTO_s.Sport.Opponent;
using Go1Bet.Infrastructure.DTO_s.Sport.SportEvent;
using Go1Bet.Infrastructure.DTO_s.Sport.SportMatch;

namespace Go1Bet.Infrastructure.Services
{
    public class UserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<RoleEntity> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly JwtService _jwtService;
        private readonly EmailService _emailService;
        private readonly AppDbContext _context;
        
        public UserService(AppDbContext context, EmailService emailService, JwtService jwtService, RoleManager<RoleEntity> roleManager, IConfiguration config, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _config = config;
            _roleManager = roleManager;
            _jwtService = jwtService;
            _emailService = emailService;
            _context = context;
        }

        public async Task<ServiceResponse> CreateAsync(CreateUserDto model)
        {
            var user = await _userManager.Users.Where(x => x.Email == model.Email).AnyAsync();
            if (user)
            {
                return new ServiceResponse
                {
                    Message = "User exists.",
                    Success = false,
                };
            }
            if (model.RefUserId != null)
            {
                var refUser = await _userManager.FindByEmailAsync(model.RefUserId);
                if (refUser != null)
                {
                    return new ServiceResponse
                    {
                        Message = "Ref User is not exists.",
                        Success = false,
                    };
                }
            }
            var mappedUser = _mapper.Map<CreateUserDto, AppUser>(model);
            mappedUser.Id = Guid.NewGuid().ToString();
            IdentityResult result = await _userManager.CreateAsync(mappedUser, model.Password);
            if (result.Succeeded)
            {
                var balance = new BalanceEntity() { Money = 300, UserId = mappedUser.Id };
                await _context.Balances.AddAsync(balance);
                await _context.SaveChangesAsync();
                mappedUser.SwitchedBalanceId = balance.Id;

                var role = model.Role != null ? model.Role : Roles.User;
                var existRole = await _roleManager.FindByNameAsync(role) != null ? role : Roles.User;
                await _userManager.AddToRoleAsync(mappedUser, existRole);

                ////Send to email confirmation token
                //var token = await _userManager.GenerateEmailConfirmationTokenAsync(mappedUser);
                //string url = $"{_config["BackEndURL"]}/api/User/ConfirmationEmail?userId={mappedUser.Id}&token={token}";
                ////IUrlHelper.Action("ResetPassword", "Account", new { token, email = mappedUser.Email }, Request.Scheme);

                //string emailBody = $"<h1>Confirm your email</h1> <a href='{url}'>Confirm now</a>";
                //await _emailService.SendEmailAsync(mappedUser.Email, "Email confirmation.", emailBody);
                //==============
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
        public async Task<ServiceResponse> ChangeUserPasswordAsync(ChangeUserPasswordDTO model)
        {

            var oldUser = await _userManager.FindByIdAsync(model.Id);
            if (oldUser != null)
            {
                var oldPassValid = await _userManager.CheckPasswordAsync(oldUser, model.OldPassword);
                if (!oldPassValid)
                {
                    return new ServiceResponse
                    {
                        Message = "Old password is not valid",
                        Success = false,
                    };
                }
                if (model.NewPassword != model.ConfirmNewPassword)
                {
                    return new ServiceResponse
                    {
                        Message = "Passwords do not match",
                        Success = false,
                    };
                }
                await _userManager.ChangePasswordAsync(oldUser, model.OldPassword, model.NewPassword);
                oldUser.DateLastPasswordUpdated = DateTime.UtcNow;
                var result = await _userManager.UpdateAsync(oldUser);
                return new ServiceResponse
                {
                    Message = "Password has been updated",
                    Success = false,
                    Payload = result,
                };
            }

            return new ServiceResponse
            {
                Message = "User not found",
                Success = false,
            };
        }
        //=========            ForgotPassword Step 1-3       ========
        public async Task<ServiceResponse> ForgotPasswordStep1Async(ForgetPasswordStep1 model)
        {
            var userExist = await _userManager.FindByEmailAsync(model.Email);
            if (userExist != null)
            {
                var rndCode = new Random().Next(100000, 999999);
                string emailBody = $"<h1>Forgot Password - Code {rndCode}</h1>";
                await _emailService.SendEmailAsync(userExist.Email, "Email confirmation.", emailBody);

                userExist.PasswordResetCode = rndCode.ToString();
                await _userManager.UpdateAsync(userExist);
                return new ServiceResponse
                {
                    Message = "The code has been sent to the email",
                    Success = true,
                };
            }
            return new ServiceResponse
            {
                Message = "User was not exist",
                Success = false,
            };
        }
        public async Task<ServiceResponse> ForgotPasswordStep2Async(ForgetPasswordStep2 model)
        {
            var userExist = await _userManager.FindByEmailAsync(model.Email);
            if (userExist != null)
            {
                if(model.ReceivedCodeFromEmail == userExist.PasswordResetCode)
                {
                    return new ServiceResponse
                    {
                        Message = "Code is valid",
                        Success = true
                    };
                }
                return new ServiceResponse
                {
                    Message = "Code is not valid",
                    Success = false
                };
            }
            return new ServiceResponse
            {
                Message = "User was not exist",
                Success = false,
            };
        }
        public async Task<ServiceResponse> ForgotPasswordStep3Async(ForgetPasswordStep3 model)
        {
            var userExist = await _userManager.FindByEmailAsync(model.Email);
            if (model.Password != model.ConfirmPassword)
            {
                return new ServiceResponse
                {
                    Message = "Passwords do not match",
                    Success = false,
                };
            }
            if (userExist != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(userExist);
                await _userManager.ResetPasswordAsync(userExist, token, model.ConfirmPassword);
                userExist.DateLastPasswordUpdated = DateTime.UtcNow;
                var result = await _userManager.UpdateAsync(userExist);
                return new ServiceResponse
                {
                    Message = "Password changed successfully",
                    Success = true
                };
            }
            return new ServiceResponse
            {
                Message = "User was not exist",
                Success = false,
            };
        }
        public async Task<ServiceResponse> ForgotUserPasswordAsync(ForgetPasswordStep3 model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                return new ServiceResponse
                {
                    Message = "Passwords do not match",
                    Success = false,
                };
            }

            var oldUser = await _userManager.FindByIdAsync(model.Email);
            if (oldUser != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(oldUser);
                await _userManager.ResetPasswordAsync(oldUser, token, model.ConfirmPassword);
                //    /\ ====== Token ===== /\
                oldUser.DateLastPasswordUpdated = DateTime.UtcNow;
                var result = await _userManager.UpdateAsync(oldUser);
                return new ServiceResponse
                {
                    Message = "Password has been updated",
                    Success = false,
                    Payload = result,
                };
            }
            return new ServiceResponse
            {
                Message = "User not found",
                Success = false,
            };
        }
        public async Task<ServiceResponse> SetRefUserByIdAsync(string userId, string refUserId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var refUser = await _userManager.FindByIdAsync(refUserId);
            if (refUser != null && user.RefUserId == null && refUser.RefUserId != user.Id)
            {
                user.RefUserId = refUser.Id;
                await _userManager.UpdateAsync(user);
                return new ServiceResponse
                {
                    Message = "The ref id is set!",
                    Success = true
                };
            }
            return new ServiceResponse
            {
                Message = "User not found or user already has a ref id",
                Success = false,
            };
        }
        public async Task<ServiceResponse> GenerateConfirmationEmailTokenAsync(string userId)
        {
            try
            {
                var userExist = await _userManager.FindByIdAsync(userId);
                if (userExist == null)
                {
                    return new ServiceResponse
                    {
                        Message = "User not found",
                        Success = false,
                    };
                }
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(userExist);

                var encodedEmailToken = Encoding.UTF8.GetBytes(token);
                //var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);
                var tokenUrl = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                return new ServiceResponse
                {
                    Message = "Email confirmation token has been generated",
                    Success = true,
                    Payload = token,
                };
            }
            catch(Exception ex) { }
            return new ServiceResponse
            {
                Message = "Something went wrong!",
                Success = false,
            };

        }
        public async Task<ServiceResponse> SendToEmail_ConfirmationTokenAsync(string email, string userId)
        {
            var userExist = await _userManager.FindByEmailAsync(email);

            if (userExist == null)
            {
                return new ServiceResponse
                {
                    Message = "User not found",
                    Success = false,
                };
            }
            return new ServiceResponse
            {
                Message = "Success",
                Success = false,
            };
        }
        public async Task<ServiceResponse> ConfirmationEmailAsync(ConfirmationEmailDTO model)
        {
            var userExist = await _userManager.FindByIdAsync(model.userId);
            if (userExist == null)
            {
                return new ServiceResponse
                {
                    Message = "User not found",
                    Success = false,
                };
            }
            await _userManager.ConfirmEmailAsync(userExist, model.token);
            return new ServiceResponse
            {
                Message = "Success",
                Success = true,
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
                };
            }
            var oldUser = await _userManager.FindByIdAsync(model.Id);
            if(oldUser != null)
            {
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
            return new ServiceResponse
            {
                Message = "User not found",
                Success = false,
            };
        }
        public async Task<ServiceResponse> UpdateUserAsync(UserEditDTO model)
        {
            var existUser = await _userManager.FindByIdAsync(model.Id.ToString());
            if (existUser != null)
            {
                if (model.RoleName != null)
                {

                    var oldRoles = await _userManager.GetRolesAsync(existUser);
                    await _userManager.RemoveFromRolesAsync(existUser, oldRoles);

                    var role = model.RoleName != null ? model.RoleName : Roles.User;
                    await _userManager.AddToRoleAsync(existUser, role);
                }
                var user = _mapper.Map(model, existUser);
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
            };
        }
        public async Task<ServiceResponse> UpdateUserPersonalInfoAsync(UserEditPersonalInfoDTO model)
        {
            var oldUser = await _userManager.FindByIdAsync(model.Id.ToString());
            if (oldUser != null)
            {
                var user = _mapper.Map(model, oldUser);
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
                    //.Where(user => user.IsDelete == false)
                    .Select(user => new UserItemDTO
                    {
                        Id = user.Id,
                        IsGoogle = user.IsGoogle,
                        IsDelete = user.IsDelete,
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
                        SwitchedBalanceId = user.SwitchedBalanceId,
                        Roles = user.UserRoles.Select(perm => new UserRoleItemDTO { RoleName = perm.Role.Name }).ToList(),
                        RefUserId = user.RefUserId,
                        CountRefUsers = user.RefUsers.Count,
                        Balances = user.Balances
                        .Select(bal => new BalanceItemDTO { Id = bal.Id, Money = bal.Money.ToString(), Reviewed = bal.Reviewed,
                            DateCreated = bal.DateCreated.ToString(),
                        }).ToList(),                       
                        CountFavouriteSportMatches = user.FavouriteSportMatches.Count()
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
                var result = await _userManager.Users.Where(user => user.Id == id)
                    .Where(user => user.IsDelete == false)
                    .Select(user => new UserItemDTO
                {
                    Id = user.Id,
                    IsGoogle = user.IsGoogle,
                    IsDelete = user.IsDelete,
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
                    SwitchedBalanceId = user.SwitchedBalanceId,
                    Roles = user.UserRoles.Select(perm => new UserRoleItemDTO { RoleName = perm.Role.Name }).ToList(),
                    RefUserId = user.RefUserId,
                    CountRefUsers = user.RefUsers.Count,
                    UsersFromRef = user.RefUsers.Select(uRef => new UserItemDTO { Id = uRef.Id, FirstName = uRef.FirstName, LastName = uRef.LastName, Email = uRef.Email, DateCreated = uRef.DateCreated.ToString() }).ToList(),
                    Promocodes = user.PromocodeUsers
                       .Select(pu => new PromocodeItemDTO 
                       { 
                           Id = pu.Promocode.Id, 
                           DateCreated = pu.Promocode.DateCreated.ToString(), 
                           ExpirationDate = pu.Promocode.ExpirationDate.ToString(),  
                           PriceMoney = pu.Promocode.PriceMoney,
                           Key = pu.Promocode.Key,
                           Name = pu.Promocode.Name,
                       }).ToList(),
                    Balances = user.Balances
                        .Select(bal => new BalanceItemDTO
                        {
                            Id = bal.Id,
                            Money = bal.Money.ToString(),
                            Reviewed = bal.Reviewed,
                            DateCreated = bal.DateCreated.ToString(),
                            Transactions = bal.TransactionHistory
                            .Select(tr => new TransactionItemDTO { Id = tr.Id, BalanceId = tr.BalanceId, Description = tr.Description, TransactionType = tr.TransactionType.ToString(), Value = tr.Value.ToString(), DateCreated = tr.DateCreated.ToString() }).ToList()
                        }).ToList(),
                    FavouriteSportMatches = user.FavouriteSportMatches.Where(fsm => fsm.UserId == user.Id)
                        .Select(fsm => new FavouriteSportMatchItemDTO
                        {
                            SportMatchId = fsm.SportMatchId,
                            UserId = fsm.User.Id,
                            SportMatchName = fsm.SportMatch.Name,
                            UserName = fsm.User.UserName,
                        }).ToList(),
                        CountFavouriteSportMatches = user.FavouriteSportMatches.Count()
                    }).ToListAsync();

                if (result != null)
                {
                    return new ServiceResponse
                    {
                        Success = true,
                        Message = "All users loaded.",
                        countPayload = result.Count,
                        Payload = result
                    };
                }
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Can't load users",
                };
            }

            return new ServiceResponse
            {
                Message = "User not found",
                Success = false,
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
            //if(user.EmailConfirmed == true)
            //{
            //    return new ServiceResponse
            //    {
            //        Success = true,
            //        Message = "User was not delete. User have Email Confirm!"
            //    };
            //}
            //var result = await _userManager.DeleteAsync(user);
            user.IsDelete = true;
            var refreshTokensByUserId = _context.RefreshToken.Where(r => r.UserId == id);
            _context.RefreshToken.RemoveRange(refreshTokensByUserId);
            var balances = await _context.Balances.Where(x=> x.UserId == id).ToListAsync();
            foreach(var balance in balances)
            {
                var tr = _context.Transactions.Where(x => x.BalanceId == balance.Id);
                _context.Transactions.RemoveRange(tr);
            }
            _context.Balances.RemoveRange(balances);
            await _context.SaveChangesAsync();
            var result = await _userManager.UpdateAsync(user);

            await _userManager.DeleteAsync(user); //Видалити цю строку коду на стадії релізу!!!!
            
            if (result.Succeeded)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Message = "User successfull deleted."
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
            try
            {
                //Install packet Google.Apis.Auth
                //So that the backend checks whether the user is authorized through Google
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>()
                {
                    _config["GoogleID:Secret"]
                }
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(model.Token, settings);
                if (payload != null)
                {
                    var info = new UserLoginInfo(model.Provider, payload.Subject, model.Provider);
                    var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
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
                            if (user.IsGoogle == true)
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

                    //var result = new { Id = user.Id, email = user.Email, firstname = user.FirstName, lastname = user.LastName, phoneNumber = user.PhoneNumber };
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
            catch (Exception ex) {
                return new ServiceResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<ServiceResponse> SwitchedBalanceId(string userId, string balanceId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (true) //user.Balances.Where(b => b.Id == balanceId).Count() == 1
            {
                user.SwitchedBalanceId = balanceId;
                await _userManager.UpdateAsync(user);
                return new ServiceResponse
                {
                    Success = false,
                    Message = "You have successfully selected your balance"
                };
            }
            return new ServiceResponse
            {
                Success = false,
                Message = "Something went wrong"
            };
        }
        public async Task<ServiceResponse> GetBettingHistory(string userId)
        {
            var bettingHistory = await _context.Bets
                    .Where(bet => bet.UserId == userId)
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
                Payload = bettingHistory
            };
        }
        public async Task<ServiceResponse> GetFavouriteSportMatches(string userId)
        {
            var sportEvents = await _context.FavouriteSportMatches
                
                .Where(fav => fav.UserId == userId)
                .Select(fav => new FavouriteSportMatchItemDTO
                {
                    UserId = fav.UserId,
                    UserName = fav.User.FirstName,
                    SportMatchId = fav.SportMatchId,
                    SportMatchName = fav.SportMatch.Name,
                    SportMatch = new SportMatchItemDTO() {
                        Id = fav.SportMatch.Id,
                        Name = fav.SportMatch.Name,
                        DateCreated = fav.SportMatch.DateCreated,
                        DateEnd = fav.SportMatch.DateEnd,
                        DateStart = fav.SportMatch.DateStart,
                        BettingFund = fav.SportMatch.Odds.Sum(x => x.Bets.Sum(b => b.Amount)),
                        CountBets = fav.SportMatch.Odds.Sum(x => x.Bets.Count()),
                        Opponents = fav.SportMatch.Opponents.Where(o => o.SportMatchId == fav.SportMatchId)
                        .Select(o => new OpponentItemDTO
                        {
                            Id = o.Id,
                            Name = o.Name,
                            SportMatchId = o.SportMatchId,
                            DateCreated = o.DateCreated,
                            Score = o.Score,
                            countTeammates = o.Teammates.Count()
                        }).ToList(),
                        //Odds
                        Odds = fav.SportMatch.Odds.Where(o => o.SportMatchId == fav.SportMatch.Id)
                        .Select(odds => new OddItemDTO
                        {
                            Id = odds.Id,
                            Name = odds.Name,
                            OpponentId = odds.OpponentId,
                            SportMatchId = odds.SportMatchId,
                            Type = odds.Type,
                            Value = odds.Value,
                            Bets = odds.Bets.Where(b => b.OddId == odds.Id).Select(b => new BetItemDTO { Id = b.Id, Amount = b.Amount, BetTime = b.BetTime.ToString("yyyy-MM-dd HH:mm"), OddId = b.OddId, UserId = b.UserId }).ToList(),
                            CountBets = odds.Bets.Count(),
                            BettingFund = odds.Bets.Sum(b => b.Amount)
                        }).ToList(),

                        SportEventId = fav.SportMatch.SportEventId,
                        SportEventName = fav.SportMatch.SportEvent.Name,
                        SportEvent = new SportEventItemDTO
                        {
                            Id = fav.SportMatch.SportEvent.Id,
                            Name = fav.SportMatch.SportEvent.Name,
                            Description = fav.SportMatch.SportEvent.Description,
                            DateCreated = fav.SportMatch.SportEvent.DateCreated,
                            DateStart = fav.SportMatch.SportEvent.DateStart,
                            DateEnd = fav.SportMatch.SportEvent.DateEnd,

                        },
                        FavouriteSportMatches = fav.SportMatch.FavouriteSportMatches.Where(fsm => fsm.UserId == userId)
                        .Select(fsm => new FavouriteSportMatchItemDTO
                        {
                            SportMatchId = fsm.SportMatchId,
                            UserId = fsm.User.Id,
                            SportMatchName = fsm.SportMatch.Name,
                            UserName = fsm.User.UserName,
                        }).ToList(),
                        CountFavouriteSportMatches = fav.SportMatch.FavouriteSportMatches.Count()
                       
                    }
                }).ToListAsync();
                
            return new ServiceResponse
            {
                Success = true,
                Payload = sportEvents
            };
        }

    }
}
