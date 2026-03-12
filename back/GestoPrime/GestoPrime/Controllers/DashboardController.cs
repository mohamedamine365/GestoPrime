using GestoPrime.Data;
using GestoPrime.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestoPrime.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context) { _context = context; }

        [HttpGet("stats-by-site")]
        public async Task<IActionResult> GetStatsBySite()
        {
            var stats = await _context.Mouvements
                .GroupBy(m => m.Site)
                .Select(g => new { Label = g.Key ?? "Inconnu", Count = g.Count() })
                .ToListAsync();

            return Ok(stats);
        }

        [HttpGet("stats-by-month")]
        public async Task<IActionResult> GetStatsByMonth()
        {
            var stats = await _context.Mouvements
                .Where(m => m.Date_Mvt != null) // On filtre les dates nulles
                .GroupBy(m => m.Date_Mvt!.Value.Month) // Le "!" dit à C# qu'on a vérifié le null
                .Select(g => new { Month = g.Key, Count = g.Count() })
                .ToListAsync();

            return Ok(stats);
        }

        [HttpGet("stats-by-day")]
        public async Task<IActionResult> GetStatsByDay()
        {
            // 1. On récupère les données brutes groupées (SQL)
            var rawData = await _context.Mouvements
                .Where(m => m.Date_Mvt != null)
                .GroupBy(m => m.Date_Mvt!.Value.Date)
                .OrderBy(g => g.Key)
                .Select(g => new { Date = g.Key, Count = g.Count() })
                .ToListAsync();

            // 2. On formate en string côté C# (Mémoire) car SQL ne connaît pas .ToString()
            var stats = rawData.Select(d => new DashboardStatDto
            {
                Label = d.Date.ToString("dd/MM/yyyy"),
                Valeur = d.Count
            }).ToList();

            return Ok(stats);
        }
    }
}