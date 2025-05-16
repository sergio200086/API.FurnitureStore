using API.FurnitureStore.Data;
using API.FurnitureStore.Share;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace API.FurnitureStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ApiFurnitureStoreContext _context;

        public ClientsController(ApiFurnitureStoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Client>> Get()
        {
            //return al clients listed in the DbContext
            return await _context.Clients.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.id == id);
            
            if (client == null) 
                return NotFound($"Client with ID {id} was not found");

            return Ok(client);
        }

        [HttpPost("postClient")]
        public async Task<IActionResult> Post(Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Post", client.id, client);
        }
        
        [HttpPut]
        public async Task<IActionResult>Put(Client client)
        {
            _context.Clients.Update(client);

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Client client)
        {
            if (client == null) return NotFound();

            _context.Clients.Remove(client);

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
