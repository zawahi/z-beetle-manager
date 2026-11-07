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
    public class BottleRecordsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BottleRecordsController(AppDbContext context)
        {
            _context = context;
        }
        
        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        [HttpGet("beetle/{beetleId}")]
        public async Task<ActionResult<IEnumerable<BottleRecord>>> GetBottleRecordsForBeetle(Guid beetleId)
        {
            var userId = GetUserId();
            // 先にカブトムシがユーザーのものか確認
            var ownsBeetle = await _context.Beetles.AnyAsync(b => b.Id == beetleId && b.UserId == userId);
            if (!ownsBeetle) return NotFound();

            return await _context.BottleRecords
                .Where(r => r.BeetleId == beetleId && r.UserId == userId)
                .OrderByDescending(r => r.ExchangeDate)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<BottleRecord>> PostBottleRecord(BottleRecord record)
        {
            var userId = GetUserId();
            var ownsBeetle = await _context.Beetles.AnyAsync(b => b.Id == record.BeetleId && b.UserId == userId);
            if (!ownsBeetle) return BadRequest("Invalid BeetleId");

            record.UserId = userId;
            _context.BottleRecords.Add(record);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBottleRecordsForBeetle", new { beetleId = record.BeetleId }, record);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBottleRecord(Guid id)
        {
            var userId = GetUserId();
            var record = await _context.BottleRecords.FirstOrDefaultAsync(r => r.Id == id && r.UserId == userId);
            
            if (record == null) return NotFound();

            _context.BottleRecords.Remove(record);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
