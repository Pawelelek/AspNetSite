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
        public async Task<IActionResult> GetAll()
        {
            var result = await _roleService.GetAllRolesAsync();
            return Ok(result);
        }
        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _roleService.GetRoleByIdAsync(id);
            return Ok(result);
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromForm] RoleCreateDTO model)
        {
            var result = await _roleService.CreateRoleAsync(model);
            return Ok(result);
        }
        [HttpPut]
        [Route("edit")]
        public async Task<IActionResult> Edit([FromForm] RoleEditDTO model)
        {
            var result = await _roleService.EditRoleAsync(model);
            return Ok(result);
        }
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _roleService.DeleteRoleByIdAsync(id);
            return Ok(result);
        }
    }
}
