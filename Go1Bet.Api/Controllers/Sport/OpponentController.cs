using Go1Bet.Infrastructure.DTO_s.Sport.Opponent;
using Go1Bet.Infrastructure.DTO_s.Sport.Person;
using Go1Bet.Infrastructure.Services.SportService;
using Microsoft.AspNetCore.Mvc;

namespace Go1Bet.Api.Controllers.Sport
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpponentController : ControllerBase
    {
        private readonly OpponentService _opponentService;
        public OpponentController(OpponentService opponentService)
        {
            _opponentService = opponentService;
        }
        [HttpGet("get")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _opponentService.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _opponentService.GetByIdAsync(id);
            return Ok(result);
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] OpponentCreateDTO model)
        {
            var result = await _opponentService.CreateAsync(model);
            return Ok(result);

        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] OpponentEditDTO model)
        {
            var result = await _opponentService.EditAsync(model);
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _opponentService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
