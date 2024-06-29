using AutoMapper;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNewsApi.Core.DTO_s.Token;
using TopNewsApi.Core.DTO_s.User;
using TopNewsApi.Core.Entities.Tokens;
using TopNewsApi.Core.Entities.User;
using TopNewsApi.Core.Interfaces;
using YoutubeDLSharp;
using YoutubeDLSharp.Options;

namespace TopNewsApi.Core.Services
{
    public class UserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly JwtService _jwtService;
        private readonly ISongUserService _songUserService;
        private readonly ISongService _songService;
        public UserService(JwtService jwtService, RoleManager<IdentityRole> roleManager, IConfiguration config, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper, ISongUserService songUserService, ISongService songService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _config = config;
            _roleManager = roleManager;
            _jwtService = jwtService;
            _songService = songService;
            _songUserService = songUserService;
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

        public async Task<ServiceResponse> GetSongsAsync(string userId)
        {
            List<Song> songList = new List<Song>();
            var res = await _songUserService.GetAll(userId);
            if (res != null)
            {
                foreach (var item in res)
                {
                    var result = await _songService.GetById(item.SongId);
                    if (!result.Success)
                    {
                        return new ServiceResponse
                        {
                            Success = false,
                            Message = "Error converting and fetching video.",
                            Payload = null
                        };
                    }
                    songList.Add(result.Payload as Song);
                }
                return new ServiceResponse
                {
                    Success = true,
                    Message = "Success.",
                    Payload = songList
                };
            }
            return new ServiceResponse
            {
                Success = false,
                Message = "res is null.",
                Payload = null
            };
        }

        public async Task<ServiceResponse> DeleteSongAsync(int id)
        {
            await _songService.Delete(id);
            return new ServiceResponse
            {
                Success = true,
                Message = "Song successfully deleted."
            };
        }

        public async Task<ServiceResponse> ConvertAndFetchVideoAsync(string videoUrl, string userId)
        {
            try
            {
                var res1 = await _songService.GetAll();
                var youtubeDl = new YoutubeDL();
                SongsDto song = new SongsDto();
                SongUserDto songUser = new SongUserDto();
                string finalUrl = "";
                var videoInfo = await youtubeDl.RunVideoDataFetch(videoUrl);
                if (videoInfo.Data == null)
                {
                    return new ServiceResponse
                    {
                        Success = false,
                        Message = "Bad url."
                    };
                }
                var songUserList = await _songUserService.GetAll(userId);
                if (songUserList != null)
                {
                    foreach (var item in songUserList)
                    {
                        var result = await _songService.GetById(item.SongId);
                        if (result.Success)
                        {
                            if ((result.Payload as Song).Name == videoInfo.Data.Title)
                            {
                                return new ServiceResponse
                                {
                                    Success = false,
                                    Message = "This song already exist in playlist"
                                };
                            }
                        }
                    }
                }
                var desiredUrl = videoInfo.Data.Formats.Where(format => format.FormatId == "251");
                foreach (var item in desiredUrl)
                {
                    finalUrl = item.Url;
                    break;
                }
                song.SongUrl = finalUrl;
                song.Name = videoInfo.Data.Title;
                await _songService.Create(song);
                songUser.UserId = userId;
                var res = await _songService.GetByName(videoInfo.Data.Title);
                songUser.SongId = (int)res.Payload;
                await _songUserService.Create(songUser);
                string[] res2 = { song.Name, song.SongUrl};             
                return new ServiceResponse
                {
                    Success = true,
                    Message = "Video successfully converted and fetched.",
                    Payload = res2
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error converting and fetching video: {ex.Message}");
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Error converting and fetching video.",
                    Payload = null
                };
            }
        }


        public async Task<ServiceResponse> GetAllAsync()
        {
            List<AppUser> users = await _userManager.Users.ToListAsync();
            List<UsersDto> mappedUsers = users.Select(u => _mapper.Map<AppUser, UsersDto>(u)).ToList();

            for (int i = 0; i < users.Count; i++)
            {
                mappedUsers[i].Role = (await _userManager.GetRolesAsync(users[i])).FirstOrDefault();
            }

            return new ServiceResponse
            {
                Success = true,
                Message = "All users loaded.",
                Payload = mappedUsers
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
                    Success = true
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

    }
}
