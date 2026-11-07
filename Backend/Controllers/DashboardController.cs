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
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        [HttpGet("stats")]
        public async Task<ActionResult<object>> GetStats()
        {
            var userId = GetUserId();
            var beetles = await _context.Beetles.Where(b => b.UserId == userId).ToListAsync();
            
            var total = beetles.Count;
            var larvaeCount = beetles.Count(b => b.Stage == LifeStage.Larva);
            var adultCount = beetles.Count(b => b.Stage == LifeStage.Adult);
            
            var activeBreeding = await _context.BreedingRecords
                .CountAsync(r => r.UserId == userId && r.AllocationDate == null);

            var threeMonthsAgo = DateTime.UtcNow.AddMonths(-3);
            
            var allBottles = await _context.BottleRecords.Where(b => b.UserId == userId).ToListAsync();
            var latestBottles = allBottles
                .GroupBy(b => b.BeetleId)
                .Select(g => g.OrderByDescending(b => b.ExchangeDate).First())
                .ToList();

            var alerts = latestBottles
                .Where(b => b.ExchangeDate < threeMonthsAgo)
                .Select(b => new {
                    BeetleId = b.BeetleId,
                    BeetleName = beetles.FirstOrDefault(x => x.Id == b.BeetleId)?.Name ?? "名称未設定",
                    LastExchangeDate = b.ExchangeDate,
                    DaysSinceExchange = (DateTime.UtcNow - b.ExchangeDate).Days
                })
                .OrderByDescending(a => a.DaysSinceExchange)
                .ToList();

            return new {
                totalBeetles = total,
                larvaeCount = larvaeCount,
                adultCount = adultCount,
                breedingActiveCount = activeBreeding,
                bottleAlerts = alerts
            };
        }
    }
}
