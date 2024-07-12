using Microsoft.AspNetCore.Mvc;
using Go1Bet.Core.DTO_s.Role;
using Go1Bet.Core.Services;

namespace Go1Bet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleService _roleService;
        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _roleService.GetAllRolesAsync();
            return Ok(result);
        }
        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var result = await _roleService.GetRoleByIdAsync(id);
            return Ok(result);
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAsync([FromForm] RoleCreateDTO model)
        {
            var result = await _roleService.CreateRoleAsync(model);
            return Ok(result);
        }
        [HttpPut]
        [Route("edit")]
        public async Task<IActionResult> EditAsync([FromForm] RoleEditDTO model)
        {
            var result = await _roleService.EditRoleAsync(model);
            return Ok(result);
        }
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var result = await _roleService.DeleteRoleByIdAsync(id);
            return Ok(result);
        }
    }
}
