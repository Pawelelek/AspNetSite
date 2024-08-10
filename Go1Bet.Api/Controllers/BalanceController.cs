using Go1Bet.Core.DTO_s.Balance;
using Go1Bet.Core.DTO_s.Role;
using Go1Bet.Core.Services;
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
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAsync([FromBody] BalanceCreateDTO model)
        {
            var result = await _balanceService.CreateAsync(model);
            return Ok(result);
        }
        [HttpPut]
        [Route("Withdrawal")]
        public async Task<IActionResult> WithdrawalAsync([FromBody] BalanceWithdrawalDTO model)
        {
            var result = await _balanceService.WithdrawalAsync(model);
            return Ok(result);
        }
        [HttpPut]
        [Route("Deposit")]
        public async Task<IActionResult> DepositAsync([FromBody] BalanceDepositDTO model)
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
