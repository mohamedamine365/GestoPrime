using GestoPrime.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestoPrime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PointageController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPointages([FromQuery] string? unit, [FromQuery] string? matricule)
        {
            try
            {
                var query = _context.V_CONSULTATION_POINTAGE.AsNoTracking().AsQueryable();

                // Si les deux paramètres reçoivent la même valeur (recherche globale)
                if (!string.IsNullOrEmpty(unit))
                {
                    var search = unit.Trim().ToLower();
                    // Utilisation de || (OU) pour chercher dans les deux colonnes à la fois
                    query = query.Where(p => p.UNITE_GESTIONNAIRE.ToLower().Contains(search)
                                          || p.MATCLE.Contains(search));
                }

                var data = await query.ToListAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur interne: {ex.Message}");
            }
        }
    }
}
