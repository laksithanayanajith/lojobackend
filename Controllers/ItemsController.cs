using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lojobackend.DbContexts;
using lojobackend.Models;

namespace lojobackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> Getitems()
        {
            return await _context.items.ToListAsync();
        }

        [HttpGet("prices")]
        public async Task<ActionResult<IEnumerable<Item>>> GetitemsByPrice()
        {
            return await _context.items.OrderBy(item => item.Price).ToListAsync();
        }

        [HttpGet("date")]
        public async Task<ActionResult<IEnumerable<Item>>> GetitemsByDate()
        {
            return await _context.items.OrderByDescending(item => item.AddedDate).ToListAsync();
        }

        // GET: api/Items/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            var item = await _context.items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpGet("filter/price")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemsByPrice([FromQuery] int stack)
        {
            List<Item> item;

            return item = stack switch
            {
                1 => await _context.items.Where(item => item.Price <= 10.00).ToListAsync(),
                2 => await _context.items.Where(item => item.Price > 10.00 && item.Price <= 20.00).ToListAsync(),
                3 => await _context.items.Where(item => item.Price > 20.00 && item.Price <= 40.00).ToListAsync(),
                4 => await _context.items.Where(item => item.Price > 40.00).ToListAsync(),
                _ => await _context.items.ToListAsync()
            };
        }

        [HttpGet("filter/category")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemsByCategory([FromQuery] string category)
        {
            return await _context.items.Where(item => item.Category.Contains(category)).ToListAsync();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemsByName([FromQuery] string name)
        {
            return await _context.items.Where(item => EF.Functions.Like(item.Name, $"%{name.ToLower()}%")).ToListAsync();
        }

        // PUT: api/Items/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(int id, Item item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Items
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(Item item)
        {
            _context.items.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItem", new { id = item.Id }, item);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _context.items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.items.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(int id)
        {
            return _context.items.Any(e => e.Id == id);
        }
    }
}
