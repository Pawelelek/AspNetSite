using AutoMapper;
using Go1Bet.Core.DTO_s.Category;
using Go1Bet.Core.Services;
using Google;
using Microsoft.AspNetCore.Mvc;

namespace Go1Bet.Api.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;


            public CategoryController(CategoryService categoryService)
            {
                _categoryService = categoryService;
            }

        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<CategoryItemDTO>>> Get()
        {
            var result = await _categoryService.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("getMainCategories")]
        public async Task<ActionResult<IEnumerable<CategoryItemDTO>>> GetMainCategoriesAsync()
        {
            var result = await _categoryService.GetMainCategoriesAsync();
            return Ok(result);
        }
        [HttpGet("get/{id}")]
        public async Task<ActionResult<IEnumerable<CategoryItemDTO>>> GetById(string id)
        {
            var result = await _categoryService.GetByIdAsync(id);
            return Ok(result);
        }
        [HttpPost("create")]
        public async Task<ActionResult<IEnumerable<CategoryCreateDTO>>> Create([FromBody] CategoryCreateDTO model)
        {
            var result = await _categoryService.Create(model);
            return Ok(result);

        }

        [HttpPut("edit")]
        public async Task<ActionResult<IEnumerable<CategoryEditDTO>>> Edit([FromBody] CategoryEditDTO model)
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
