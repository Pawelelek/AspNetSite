using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Go1Bet.Infrastructure.DTO_s.User;
using Go1Bet.Infrastructure.Services;
using Go1Bet.Infrastructure.Validations.User;
using Go1Bet.Infrastructure.DTO_s.Token;
using Go1Bet.Infrastructure.DTO_s.User.ForgetPassword;


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
        public async Task<IActionResult> GoogleExternalLoginAsync([FromBody] GoogleExternalLoginDTO model)
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
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserDto model)
        {
            var result = await _userService.CreateAsync(model);
            return Ok(result);
        }
        [HttpPut("Edit")]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UserEditDTO model)
        {
            var result = await _userService.UpdateUserAsync(model);
            return Ok(result);
        }
        [HttpPut("UpdatePersonalInfo")]
        public async Task<IActionResult> UpdatePersonalInfoAsync([FromBody] UserEditPersonalInfoDTO model)
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
        public async Task<IActionResult> UpdateEmailAsync([FromBody] UserEditEmailDTO model)
        {
            var result = await _userService.UpdateUserEmailAsync(model);
            return Ok(result);
        }
        [HttpGet("GetPasswordResetTokenByUserId")]
        public async Task<IActionResult> GetPasswordResetTokenAsync(string userId)
        {
            var result = await _userService.GetPasswordResetTokenAsync(userId);
            return Ok(result);
        }
        [HttpPost("ForgotPasswordStep1")]
        public async Task<IActionResult> ForgotPasswordStep1(ForgetPasswordStep1 model)
        {
            var result = await _userService.ForgotPasswordStep1Async(model);
            return Ok(result);
        }
        [HttpPost("ForgotPasswordStep2")]
        public async Task<IActionResult> ForgotPasswordStep2(ForgetPasswordStep2 model)
        {
            var result = await _userService.ForgotPasswordStep2Async(model);
            return Ok(result);
        }
        [HttpPost("ForgotPasswordStep3")]
        public async Task<IActionResult> ForgotPasswordStep3(ForgetPasswordStep3 model)
        {
            var result = await _userService.ForgotPasswordStep3Async(model);
            return Ok(result);
        }
        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChanePasswordAsync([FromBody] ChangeUserPasswordDTO model)
        {
            var result = await _userService.ChangeUserPasswordAsync(model);
            return Ok(result);
        }
        [HttpPut("UpdateUserRole")]
        public async Task<IActionResult> UpdateUserRoleAsync([FromBody] UserEditRoleDTO model)
        {
            var result = await _userService.UpdateUserRoleAsync(model);
            return Ok(result);
        }
        [HttpPut("SwitchedBalanceId")]
        public async Task<IActionResult> SwitchedBalanceId(string userId, string balanceId)
        {
            var result = await _userService.SwitchedBalanceId(userId, balanceId);
            return Ok(result);
        }
        [HttpPut("SetRefUserById")]
        public async Task<IActionResult> SetRefUserByIdAsync(string userId, string refUserId)
        {
            var result = await _userService.SetRefUserByIdAsync(userId, refUserId);
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
        [HttpGet("GetGenerateConfirmationEmailToken")]
        public async Task<IActionResult> GenerateConfirmationEmailTokenAsync(string userId)
        {
            var result = await _userService.GenerateConfirmationEmailTokenAsync(userId);
            return Ok(result);
        }
        [HttpPost("ConfirmationEmail")]
        public async Task<IActionResult> ConfirmationEmailTokenAsync([FromQuery] ConfirmationEmailDTO model)
        {
            var result = await _userService.ConfirmationEmailAsync(model);
            return Ok(result);
        }
        [HttpGet("GetBettingHistory")]
        public async Task<IActionResult> GetBettingHistory(string userId)
        {
            var result = await _userService.GetBettingHistory(userId);
            return Ok(result);
        }
        [HttpGet("GetFavouriteSportMatches")]
        public async Task<IActionResult> GetFavouriteSportMatches(string userId)
        {
            var result = await _userService.GetFavouriteSportMatches(userId);
            return Ok(result);
        }
    }
}
