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
            return await _context.items
                                    .Select(item => new Item
                                    {
                                        Id = item.Id,
                                        Name = item.Name,
                                        Description = item.Description,
                                        Price = item.Price,
                                        Category = item.Category,
                                        DefaultImage = _context.images.Where(image => image.ItemId == item.Id).Select(image => image.url).FirstOrDefault(),
                                        AddedDate = item.AddedDate
                                    })
                                    .ToArrayAsync();
        }

        [HttpGet("prices")]
        public async Task<ActionResult<IEnumerable<Item>>> GetitemsByPrice()
        {
            return await _context.items
                                    .Select(item => new Item
                                    {
                                        Id = item.Id,
                                        Name = item.Name,
                                        Description = item.Description,
                                        Price = item.Price,
                                        Category = item.Category,
                                        DefaultImage = _context.images.Where(image => image.ItemId == item.Id).Select(image => image.url).FirstOrDefault(),
                                        AddedDate = item.AddedDate
                                    })
                                    .OrderBy(item => item.Price)
                                    .ToArrayAsync();
        }

        [HttpGet("date")]
        public async Task<ActionResult<IEnumerable<Item>>> GetitemsByDate()
        {
            return await _context.items
                                    .Select(item => new Item
                                    {
                                        Id = item.Id,
                                        Name = item.Name,
                                        Description = item.Description,
                                        Price = item.Price,
                                        Category = item.Category,
                                        DefaultImage = _context.images.Where(image => image.ItemId == item.Id).Select(image => image.url).FirstOrDefault(),
                                        AddedDate = item.AddedDate
                                    })
                                    .OrderByDescending(item => item.AddedDate)
                                    .ToArrayAsync();
        }

        // GET: api/Items/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            var item = await _context.items
                .Select(item => new Item
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    Category = item.Category,
                    AddedDate = item.AddedDate,
                    DefaultImage = _context.images.Where(image => image.ItemId == item.Id).Select(image => image.url).FirstOrDefault()
                })
                .FirstOrDefaultAsync(item => item.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }


        [HttpGet("filter/price")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemsByPrice([FromQuery] int stack)
        {
            var itemsWithImages = await _context.items
                        .Select(item => new Item
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Description = item.Description,
                            Price = item.Price,
                            Category = item.Category,
                            DefaultImage = _context.images
                                .Where(image => image.ItemId == item.Id)
                                .Select(image => image.url)
                                .FirstOrDefault(),
                            AddedDate = item.AddedDate
                        })
                        .Where(item =>
                            stack == 1 ? item.Price <= 155.00 :
                            stack == 2 ? item.Price > 155.00 && item.Price <= 160.00 :
                            stack == 3 ? item.Price > 161.00 && item.Price <= 185.00 :
                            item.Price > 190.00
                        )
                        .ToArrayAsync();

            return itemsWithImages;

        }

        [HttpGet("filter/category")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemsByCategory([FromQuery] string category)
        {
            return await _context.items
                                    .Select(item => new Item
                                    {
                                        Id = item.Id,
                                        Name = item.Name,
                                        Description = item.Description,
                                        Price = item.Price,
                                        Category = item.Category,
                                        DefaultImage = _context.images.Where(image => image.ItemId == item.Id).Select(image => image.url).FirstOrDefault(),
                                        AddedDate = item.AddedDate
                                    })
                                    .Where(item => item.Category.Contains(category))
                                    .ToArrayAsync();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemsByName([FromQuery] string name)
        {
            return await _context.items
                .Where(item => EF.Functions.Like(item.Name, $"%{name.ToLower()}%"))
                .Select(item => new Item
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    Category = item.Category,
                    DefaultImage = _context.images.Where(image => image.ItemId == item.Id).Select(image => image.url).FirstOrDefault(),
                    AddedDate = item.AddedDate
                })
                .ToListAsync();
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
