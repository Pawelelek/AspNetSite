using Go1Bet.Infrastructure.DTO_s.Sport.Opponent;
using Go1Bet.Infrastructure.DTO_s.Sport.SportEvent;
using Go1Bet.Infrastructure.Services.SportService;
using Microsoft.AspNetCore.Mvc;

namespace Go1Bet.Api.Controllers.Sport
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportEventController : ControllerBase
    {
        private readonly SportEventService _sportEventService;
        public SportEventController(SportEventService sportEventService)
        {
            _sportEventService = sportEventService;
        }
        [HttpGet("get")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _sportEventService.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _sportEventService.GetByIdAsync(id);
            return Ok(result);
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] SportEventCreateDTO model)
        {
            var result = await _sportEventService.CreateAsync(model);
            return Ok(result);

        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] SportEventEditDTO model)
        {
            var result = await _sportEventService.EditAsync(model);
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _sportEventService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
