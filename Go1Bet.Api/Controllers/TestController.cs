using Go1Bet.Infrastructure.DTO_s.Sport.Bet;
using Go1Bet.Infrastructure.Services.SportService;
using Microsoft.AspNetCore.Mvc;

namespace Go1Bet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("get1")]
        public async Task<IActionResult> Get1()
        {
            var result = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm");
            return Ok(result);
        }
        [HttpGet("get2")]
        public async Task<IActionResult> Get2()
        {
            DateTime originalDateTime = DateTime.UtcNow;
            DateTime result = new DateTime(
                originalDateTime.Year,
                originalDateTime.Month,
                originalDateTime.Day,
                originalDateTime.Hour,
                originalDateTime.Minute,
                originalDateTime.Second
            );
            return Ok(result);
        }
    }
}
