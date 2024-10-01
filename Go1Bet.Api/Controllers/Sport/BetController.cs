using Go1Bet.Infrastructure.DTO_s.Sport.Bet;
using Go1Bet.Infrastructure.DTO_s.Sport.Odd;
using Go1Bet.Infrastructure.Services.SportService;
using Microsoft.AspNetCore.Mvc;

namespace Go1Bet.Api.Controllers.Sport
{
    [Route("api/[controller]")]
    [ApiController]
    public class BetController : ControllerBase
    {
        private readonly BetService _betService;
        public BetController(BetService betService)
        {
            _betService = betService;
        }
        [HttpGet("get")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _betService.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _betService.GetByIdAsync(id);
            return Ok(result);
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] BetCreateDTO model)
        {
            var result = await _betService.CreateAsync(model);
            return Ok(result);

        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] BetEditDTO model)
        {
            var result = await _betService.EditAsync(model);
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _betService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
