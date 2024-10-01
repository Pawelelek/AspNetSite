using Go1Bet.Infrastructure.DTO_s.Sport.Odd;
using Go1Bet.Infrastructure.DTO_s.Sport.Opponent;
using Go1Bet.Infrastructure.Services.SportService;
using Microsoft.AspNetCore.Mvc;

namespace Go1Bet.Api.Controllers.Sport
{
    [Route("api/[controller]")]
    [ApiController]
    public class OddController : ControllerBase
    {
        private readonly OddService _oddService;
        public OddController(OddService oddService)
        {
            _oddService = oddService;
        }
        [HttpGet("get")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _oddService.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _oddService.GetByIdAsync(id);
            return Ok(result);
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] OddCreateDTO model)
        {
            var result = await _oddService.CreateAsync(model);
            return Ok(result);

        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] OddEditDTO model)
        {
            var result = await _oddService.EditAsync(model);
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _oddService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
