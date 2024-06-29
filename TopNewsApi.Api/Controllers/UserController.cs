using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopNewsApi.Core.DTO_s.User;
using TopNewsApi.Core.Services;
using TopNewsApi.Core.Validations.User;
using TopNewsApi.Core.DTO_s.Token;
using YoutubeDLSharp;
using YoutubeDLSharp.Options;
using TopNewsApi.Core.Interfaces;
using TopNewsApi.Core.Validations.Songs;

namespace TopNewsApi.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ISongUserService _songUserService;
        private readonly ISongService _songService;

        public UserController(UserService userService, ISongUserService songUserService, ISongService songService)
        {
            _userService = userService;
            _songUserService = songUserService;
            _songService = songService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("GetAllSongs")]
        public async Task<IActionResult> GetAllSongs()
        {
            var result = await _songService.GetAll();
            return Ok(result);
        }

        [HttpPost("DeleteSong")]
        public async Task<IActionResult> DeleteSong(int songId)
        {
            try
            {
                await _userService.DeleteSongAsync(songId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Помилка: {ex.Message}");
            }
        }

        [HttpGet("GetSongs")]
        public async Task<IActionResult> GetSongs(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest("Помилковий запит");
            }

            try
            {
                var answer = await _userService.GetSongsAsync(userId);

                if (answer.Success)
                {
                    return Ok(answer);
                }
                else
                {
                    return BadRequest("Не вдалося обробити відео");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Помилка сервера: {ex.Message}");
            }
        }

        [HttpPost("ConvertAndFetchVideo")]
        public async Task<IActionResult> ConvertAndFetchVideo([FromBody] string videoUrl, string userId)
        {
            //if (string.IsNullOrWhiteSpace(videoUrl))
            //{
            //    return BadRequest("Помилковий запит");
            //}

            try
            {
                var audioBytes = await _userService.ConvertAndFetchVideoAsync(videoUrl, userId);

                if (audioBytes != null)
                {
                    return Ok(audioBytes);
                }
                else
                {
                    return BadRequest("Не вдалося обробити відео");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Помилка сервера: {ex.Message}");
            }
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
                return NotFound();
            }
            return NotFound();
        }

        [HttpPost("AddSong")]
        public async Task<IActionResult> AddSong(SongsDto model)
        {
            var validator = new SongAddValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                await _songService.Create(model);
            }
            return NotFound();
        }

        [HttpPost("DeleteById")]
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
