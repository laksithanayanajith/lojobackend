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
    public class ImagesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ImagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Images/5
        [HttpGet("image")]
        public async Task<ActionResult<string>> GetImage([FromQuery]int itemId, [FromQuery]string sizeName, [FromQuery]string colorName)
        {
            Item? item = await _context.items.
                Include(item => item.Colors)
                .Include(item => item.Images)
                .Include(item => item.Sizes)
                .Where(item => item.Sizes.Any(size => size.SizeName.Equals(sizeName)))
                .Where(item => item.Colors.Any(color => color.Name.Equals(colorName)))
                .FirstOrDefaultAsync(item => item.Id == itemId);

            return item?.Images?.Select(image => image.url).AsQueryable().FirstOrDefault()!;
        }

        // POST: api/Images
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Image>> PostImage(Image image)
        {
            _context.images.Add(image);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImage", new { id = image.Id }, image);
        }

        // DELETE: api/Images/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var image = await _context.images.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }

            _context.images.Remove(image);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImageExists(int id)
        {
            return _context.images.Any(e => e.Id == id);
        }
    }
}
