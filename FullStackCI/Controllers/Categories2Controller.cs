using Asp.Versioning;
using FullStackCI.Dtos;
using FullStackCI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FullStackCI.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class Categories2Controller(ICategoryService categoryService) : CategoriesController(categoryService)
    {
        private readonly ICategoryService _categoryService = categoryService;

        [MapToApiVersion(2.0)]
        [HttpGet("{id}")]
        public new async Task<ActionResult<CategoryDtoV2>> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdV2Async(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
    }
}
