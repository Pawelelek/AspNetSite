using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopNewsApi.Core.DTO_s.User;
using TopNewsApi.Core.Services;
using TopNewsApi.Core.Validations.User;
using TopNewsApi.Core.DTO_s.Token;
using TopNewsApi.Core.Interfaces;

namespace TopNewsApi.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAllAsync();
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateUserDto model)
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

        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateUserDto model)
        {
            var validator = new UpdateUserValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                var result = await _userService.UpdateAsync(model);
                if (result.Success)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            return NotFound();
        }

        [HttpDelete("DeleteById")]
        public async Task<IActionResult> DeleteById(string id)
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
