using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Category;
using Web.Commom.Request;
using Web.Service.IService;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] GetWithPaginationQuery query, CancellationToken cancellationToken)
        {
            var categories = await _categoryService.GetAllCategories(query, cancellationToken);

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id, CancellationToken cancellationToken)
        {
            var category = await _categoryService.GetCategoryById(id, cancellationToken);

            return Ok(category);
        }

        //[Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromForm] CreateCategoryDto request, CancellationToken cancellationToken)
        {
            var category = await _categoryService.CreateCategory(request, cancellationToken);

            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryDto request, CancellationToken cancellationToken)
        {
            var category = await _categoryService.UpdateCategory(id, request, cancellationToken);

            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id, CancellationToken cancellationToken)
        {
            await _categoryService.DeleteCategory(id, cancellationToken);
            return NoContent();
        }
    }
}
