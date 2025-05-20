using API.FurnitureStore.Data;
using API.FurnitureStore.Share;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.FurnitureStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApiFurnitureStoreContext _context;

        public CategoriesController(ApiFurnitureStoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _context.ProductCategories.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _context.ProductCategories.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null) 
                return NotFound();

            return Ok(category);
        }

        [HttpPost("postCategory")]
        public async Task<IActionResult> NewCategory(ProductCategory category)
        {
            await _context.ProductCategories.AddAsync(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("NewCategory", category.Id, category);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(ProductCategory category)
        {
            _context.ProductCategories.Update(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(ProductCategory category)
        {
            if (category == null)
                return NotFound();
            _context.ProductCategories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("deleteCategories")]
        public async Task<IActionResult> DeleteCategories(ProductCategory[] categories)
        {
            if (!categories.Any()) return NotFound();

            _context.ProductCategories.RemoveRange(categories);
            await _context.SaveChangesAsync();

            return NoContent();

        }

    }
}
