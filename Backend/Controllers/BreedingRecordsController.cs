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
    public class BreedingRecordsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BreedingRecordsController(AppDbContext context)
        {
            _context = context;
        }

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BreedingRecord>>> GetBreedingRecords()
        {
            var userId = GetUserId();
            return await _context.BreedingRecords
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.StartDate)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<BreedingRecord>> PostBreedingRecord(BreedingRecord record)
        {
            var userId = GetUserId();
            record.UserId = userId;
            _context.BreedingRecords.Add(record);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBreedingRecords), new { id = record.Id }, record);
        }
    }
}
