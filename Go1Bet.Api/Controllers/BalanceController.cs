using Go1Bet.Infrastructure.DTO_s.Balance;
using Go1Bet.Infrastructure.DTO_s.Role;
using Go1Bet.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Go1Bet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        private readonly BalanceService _balanceService;
        public BalanceController(BalanceService balanceService)
        {
            _balanceService = balanceService;
        }
        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _balanceService.GetAllAsync();
            return Ok(result);
        }
        [HttpGet]
        [Route("getById")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var result = await _balanceService.GetByIdAsync(id);
            return Ok(result);
        }
        [HttpGet]
        [Route("getByUserId")]
        public async Task<IActionResult> GetByUserIdAsync(string userId)
        {
            var result = await _balanceService.GetByUserIdAsync(userId);
            return Ok(result);
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAsync([FromBody] BalanceCreateDTO model)
        {
            var result = await _balanceService.CreateAsync(model);
            return Ok(result);
        }
        [HttpPut]
        [Route("Withdrawal")]
        public async Task<IActionResult> WithdrawalAsync([FromBody] BalanceInteractionDTO model)
        {
            var result = await _balanceService.WithdrawalAsync(model);
            return Ok(result);
        }
        [HttpPut]
        [Route("Deposit")]
        public async Task<IActionResult> DepositAsync([FromBody] BalanceInteractionDTO model)
        {
            var result = await _balanceService.DepositAsync(model);
            return Ok(result);
        }
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var result = await _balanceService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
