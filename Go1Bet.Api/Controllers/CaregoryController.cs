using Go1Bet.Infrastructure.DTO_s.Category;
using Go1Bet.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Go1Bet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;


            public CategoryController(CategoryService categoryService)
            {
                _categoryService = categoryService;
            }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var result = await _categoryService.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("getMainCategories")]
        public async Task<IActionResult> GetMainCategoriesAsync()
        {
            var result = await _categoryService.GetMainCategoriesAsync();
            return Ok(result);
        }
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _categoryService.GetByIdAsync(id);
            return Ok(result);
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDTO model)
        {
            var result = await _categoryService.Create(model);
            return Ok(result);

        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] CategoryEditDTO model)
        {
            var result = await _categoryService.EditCategoryAsync(model);
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            return Ok(result);
        }
    }
}
