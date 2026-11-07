using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using KuwagataApp.Backend.Data;
using KuwagataApp.Backend.Models;

namespace KuwagataApp.Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BeetlesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BeetlesController(AppDbContext context)
        {
            _context = context;
        }

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Beetle>>> GetBeetles()
        {
            var userId = GetUserId();
            return await _context.Beetles.Where(b => b.UserId == userId).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Beetle>> GetBeetle(Guid id)
        {
            var userId = GetUserId();
            var beetle = await _context.Beetles
                .Include(b => b.BottleRecords)
                .Include(b => b.Sire)
                .Include(b => b.Dam)
                .FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);

            if (beetle == null) return NotFound();

            return beetle;
        }

        [HttpPost]
        public async Task<ActionResult<Beetle>> PostBeetle(Beetle beetle)
        {
            beetle.UserId = GetUserId();
            _context.Beetles.Add(beetle);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBeetle), new { id = beetle.Id }, beetle);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBeetle(Guid id, Beetle beetle)
        {
            if (id != beetle.Id) return BadRequest();

            beetle.UserId = GetUserId();
            _context.Entry(beetle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Beetles.Any(e => e.Id == id && e.UserId == beetle.UserId))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBeetle(Guid id)
        {
            var userId = GetUserId();
            var beetle = await _context.Beetles.FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);
            if (beetle == null) return NotFound();

            _context.Beetles.Remove(beetle);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
