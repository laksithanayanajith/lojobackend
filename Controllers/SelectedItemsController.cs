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
    public class SelectedItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SelectedItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SelectedItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SelectedItem>>> GetSelectedItem()
        {
            return await _context.SelectedItem.ToListAsync();
        }

        [HttpGet("Subtotal")]
        public async Task<ActionResult<double>> GetSelectedItemSubtotal()
        {
            return await _context.SelectedItem.SumAsync(item => item.TotalPrice);
        }

        // GET: api/SelectedItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SelectedItem>> GetSelectedItem(int id)
        {
            var selectedItem = await _context.SelectedItem.FindAsync(id);

            if (selectedItem == null)
            {
                return NotFound();
            }

            return selectedItem;
        }

        // PUT: api/SelectedItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSelectedItem(int id, SelectedItem selectedItem)
        {
            if (id != selectedItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(selectedItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SelectedItemExists(id))
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

        // POST: api/SelectedItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SelectedItem>> PostSelectedItem(SelectedItem selectedItem)
        {
            _context.SelectedItem.Add(selectedItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSelectedItem", new { id = selectedItem.Id }, selectedItem);
        }

        // DELETE: api/SelectedItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSelectedItem(int id)
        {
            var selectedItem = await _context.SelectedItem.FindAsync(id);
            if (selectedItem == null)
            {
                return NotFound();
            }

            _context.SelectedItem.Remove(selectedItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SelectedItemExists(int id)
        {
            return _context.SelectedItem.Any(e => e.Id == id);
        }
    }
}
