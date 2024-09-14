using Go1Bet.Infrastructure.DTO_s.Sport.SportEvent;
using Go1Bet.Infrastructure.DTO_s.Sport.SportMatch;
using Go1Bet.Infrastructure.Services.SportService;
using Microsoft.AspNetCore.Mvc;

namespace Go1Bet.Api.Controllers.Sport
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportMatchController : ControllerBase
    {
        private readonly SportMatchService _sportMatchService;
        public SportMatchController(SportMatchService sportMatchService)
        {
            _sportMatchService = sportMatchService;
        }
        [HttpGet("get")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _sportMatchService.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _sportMatchService.GetByIdAsync(id);
            return Ok(result);
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] SportMatchCreateDTO model)
        {
            var result = await _sportMatchService.CreateAsync(model);
            return Ok(result);

        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] SportMatchEditDTO model)
        {
            var result = await _sportMatchService.EditAsync(model);
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _sportMatchService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
