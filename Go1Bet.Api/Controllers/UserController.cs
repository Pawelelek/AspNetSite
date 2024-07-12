using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Go1Bet.Core.DTO_s.User;
using Go1Bet.Core.Services;
using Go1Bet.Core.Validations.User;
using Go1Bet.Core.DTO_s.Token;
using Go1Bet.Core.Interfaces;

namespace Go1Bet.Api.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        [Route("GoogleExternalLogin")]
        public async Task<IActionResult> GoogleExternalLoginAsync([FromForm] GoogleExternalLoginDTO model)
        {
            var result = await _userService.GoogleExternalLogin(model);
            return Ok(result);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _userService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetByIdAsync(string Id)
        {
            var result = await _userService.GetByIdAsync(Id);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync(CreateUserDto model)
        {
            var validator = new CreateUserValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                var result = await _userService.CreateAsync(model);
                if (result.Success)
                {
                    return Ok(result);
                }
                return NotFound("Error while creating user");
            }
            return NotFound("validation problem");
        }
        //1. UPDATE > Name, LastName, PhoneNumber
        //2. UPDATE > Password
        //3. UPDATE > Email
        [HttpPut("Update")]
        public async Task<IActionResult> UpdatePersonalInfoAsync(UserEditDTO model)
        {
            var validator = new UpdateUserValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                var result = await _userService.UpdateUserPersonalInfoAsync(model);
                if (result.Success)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            return NotFound();
        }
        [HttpPut("UpdateEmail")]
        public async Task<IActionResult> UpdateEmailAsync(UserEditEmailDTO model)
        {
            var result = await _userService.UpdateUserEmailAsync(model);
            return Ok(result);
        }
        [HttpPut("UpdatePassword")]
        public async Task<IActionResult> UpdatePasswordAsync(UserEditPasswordDTO model)
        {
            var result = await _userService.UpdateUserPasswordAsync(model);
            return Ok(result);
        }
        [HttpPut("UpdateUserRole")]
        public async Task<IActionResult> UpdateUserRoleAsync(UserEditRoleDTO model)
        {
            var result = await _userService.UpdateUserRoleAsync(model);
            return Ok(result);
        }

        [HttpDelete("DeleteById")]
        public async Task<IActionResult> DeleteByIdAsync(string id)
        {
            var result = await _userService.DeleteAsync(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserDto model)
        {
            var validator = new  LoginUserValidation();
            var validationResult = await validator.ValidateAsync(model);
            if(validationResult.IsValid)
            {
                var result = await _userService.LoginUserAsync(model);
                return Ok(result);
            }

            return BadRequest(validationResult.Errors[0].ToString());
        }

        [HttpGet("logout")]
        public async Task<IActionResult> SignOutAsync(string userId)
        {            
            var result = await _userService.LogoutUserAsync(userId);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] TokenRequestDto model)
        {
            var result = await _userService.RefreshTokenAsync(model);
            return Ok(result);
        }
    }
}
