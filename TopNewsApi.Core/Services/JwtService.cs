using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TopNewsApi.Core.DTO_s.Token;
using TopNewsApi.Core.Entities.Specifications;
using TopNewsApi.Core.Entities.Tokens;
using TopNewsApi.Core.Entities.User;
using TopNewsApi.Core.Interfaces;

namespace TopNewsApi.Core.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<RefreshToken> _tokenRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenValidationParameters _validationParameters;

        public JwtService(TokenValidationParameters validationParameters, IConfiguration configuration, IRepository<RefreshToken> tokenRepo, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _tokenRepo = tokenRepo;
            _userManager = userManager;
            _validationParameters = validationParameters;
        }

        public async Task Create(RefreshToken token)
        {
            await _tokenRepo.Insert(token);
            await _tokenRepo.Save();
        }

        public async Task Delete(RefreshToken token)
        {
            await _tokenRepo.Delete(token);
            await _tokenRepo.Save();
        }

        public async Task<RefreshToken?> Get(string token)
        {
            var result = await _tokenRepo.GetListBySpec(new RefreshTokenSpecification.GetRefreshToken(token));
            return (RefreshToken)result.FirstOrDefault();
        }

        public async Task<IEnumerable<RefreshToken?>> GetById(string userId)
        {
            var result = await _tokenRepo.GetListBySpec(new RefreshTokenSpecification.GetAllTokens(userId));
            return result;
        }

        public async Task<IEnumerable<RefreshToken>> GetAll()
        {
            IEnumerable<RefreshToken> result = await _tokenRepo.GetAll();
            return result;
        }

        public async Task Update(RefreshToken token)
        {
            await _tokenRepo.Update(token);
            await _tokenRepo.Save();
        }


        public async Task<Tokens> GenerateJwtTokensAsync(AppUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtConfig:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
               {
                    new Claim("Id", user.Id),
                    new Claim("Name", user.FirstName),
                    new Claim("Surname", user.LastName),
                    new Claim("Email", user.Email),
                    new Claim("EmailConfirm", user.EmailConfirmed.ToString()),
                    new Claim("PhoneNumber", user.PhoneNumber ?? ""),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, roles[0]),
                    new Claim(JwtRegisteredClaimNames.Aud, _configuration["JwtConfig:Audience"]),
                    new Claim(JwtRegisteredClaimNames.Iss, _configuration["JwtConfig:Issuer"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };


            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);


            var refreshToken = new RefreshToken()
            {
                JwtId = token.Id,
                IsUsed = false,
                UserId = user.Id,
                AddedDate = DateTime.UtcNow,
                ExpireDate = DateTime.UtcNow.AddDays(1),
                IsRevoked = false,
                Token = RandomString(25) + Guid.NewGuid()
            };

            await Create(refreshToken);

            var tokens = new Tokens();
            tokens.Token = jwtToken;
            tokens.refreshToken = refreshToken;

            return tokens;


        }


        public async Task<ServiceResponse> VerifyTokenAsync(TokenRequestDto tokenRequest)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                _validationParameters.ValidateLifetime = false;
                var principal = jwtTokenHandler.ValidateToken(tokenRequest.Token, _validationParameters, out var validatedToken);
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase);
                    if (result == false)
                    {
                        return null;
                    }
                }

                var utcExpireDate = long.Parse(principal.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                var expDate = UnixTimeStampToDateTime(utcExpireDate);
                if (expDate > DateTime.UtcNow)
                {
                    return new ServiceResponse
                    {
                        Errors = new List<string>() { "Cannot refresh token. Token expired." },
                        Success = false
                    };
                }

                var storedToken = await Get(tokenRequest.RefreshToken);
                if (storedToken == null)
                {
                    return new ServiceResponse
                    {
                        Errors = new List<string>() { "Refrest token not found." },
                        Success = false
                    };
                }

                if (DateTime.UtcNow > storedToken.ExpireDate)
                {
                    return new ServiceResponse
                    {
                        Errors = new List<string>() { "Token has been expired." },
                        Success = false
                    };
                }

                if (storedToken.IsUsed)
                {
                    return new ServiceResponse
                    {
                        Errors = new List<string>() { "Token has been used." },
                        Success = false
                    };
                }

                if (storedToken.IsRevoked)
                {
                    return new ServiceResponse
                    {
                        Errors = new List<string>() { "Token has been revoked." },
                        Success = false
                    };
                }

                var jti = principal.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
                if (storedToken.JwtId != jti)
                {

                    return new ServiceResponse
                    {
                        Errors = new List<string>() { "Token doesn't match the saved token." },
                        Success = false
                    };
                }

                storedToken.IsUsed = true;
                await Update(storedToken);
                var dbUser = await _userManager.FindByIdAsync(storedToken.UserId);
                var tokens = await GenerateJwtTokensAsync(dbUser);

                return new ServiceResponse
                {
                    Message = "Token successfully updated.",
                    Success = true,
                    AccessToken = tokens.Token,
                    RefreshToken = tokens.refreshToken.Token
                };


            }
            catch (Exception ex)
            {

                return new ServiceResponse
                {
                    Message = ex.Message,
                    Success = false
                };
            }
        }

        private DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToUniversalTime();
            return dtDateTime;
        }


        private string RandomString(int langth)
        {
            var random = new Random();
            var chars = "QWERTYUIOPASDFGHJKLZXCVBNM0123456789";
            return new string(Enumerable.Repeat(chars, langth).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

    public class Tokens
    {
        public string Token { get; set; } = String.Empty;
        public RefreshToken refreshToken { get; set; }
    }
}
