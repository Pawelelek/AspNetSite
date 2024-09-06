using Go1Bet.Infrastructure.DTO_s.Balance;
using Go1Bet.Infrastructure.DTO_s.Bonus;
using Go1Bet.Infrastructure.DTO_s.Bonus.Promocode;
using Go1Bet.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Go1Bet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BonusController : ControllerBase
    {
        private readonly BonusService _bonuseService;
        public BonusController(BonusService bonusService)
        {
            _bonuseService = bonusService;
        }
        [HttpGet]
        [Route("getAllPromocodes")]
        public async Task<IActionResult> GetAllPromocodesAsync()
        {
            var result = await _bonuseService.GetAllPromocodesAsync();
            return Ok(result);
        }
        [HttpGet]
        [Route("getPromocodeById")]
        public async Task<IActionResult> GetPromocodeByIdAsync(string id)
        {
            var result = await _bonuseService.GetPromocodeByIdAsync(id);
            return Ok(result);
        }   
        [HttpPost]
        [Route("CreatePromocode")]
        public async Task<IActionResult> CreatePromocodeAsync([FromBody] PromocodeCreateDTO model)
        {
            var result = await _bonuseService.CreatePromocodeAsync(model);
            return Ok(result);
        }
        [HttpPost]
        [Route("ActivePromocodeByUser")]
        public async Task<IActionResult> ActivePromocodeAsyncByUserAsync([FromBody] PromocodeActiveDTO model)
        {
            var result = await _bonuseService.ActivePromocodeAsync(model);
            return Ok(result);
        }
        [HttpPut]
        [Route("EditPromocodeById")]
        public async Task<IActionResult> EditPromocodeById([FromBody] PromocodeEditDTO model)
        {
            var result = await _bonuseService.EditPromocodeAsync(model);
            return Ok(result);
        }
        [HttpDelete]
        [Route("DeletePromoById")]
        public async Task<IActionResult> DeletePromoByIdAsync(string id)
        {
            var result = await _bonuseService.DeletePromoByIdAsync(id);
            return Ok(result);
        }
    }
}
