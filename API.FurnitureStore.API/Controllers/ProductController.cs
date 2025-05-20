using API.FurnitureStore.Data;
using API.FurnitureStore.Share;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.FurnitureStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApiFurnitureStoreContext _context;

        public ProductController(ApiFurnitureStoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var result = await _context.Products.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null) 
                return NotFound();

            return Ok(product);
        }

        [HttpPost("postProduct")]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("CreateProduct", product.Id, product);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(Product product)
        {
            if (product == null)
                return NotFound();
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("deleteCategories")]
        public async Task<IActionResult> DeleteCategories(Product[] products)
        {
            if (products.Length == 0) return NotFound();

            _context.Products.RemoveRange(products);
            await _context.SaveChangesAsync();

            return NoContent();

        }

    }
}
