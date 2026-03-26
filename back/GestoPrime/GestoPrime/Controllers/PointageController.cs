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
                // 1. On prépare la requête
                var query = _context.V_CONSULTATION_POINTAGE.AsNoTracking().AsQueryable();

                // 2. Filtre par Unité (Insensible à la casse)
                if (!string.IsNullOrEmpty(unit))
                {
                    var searchUnit = unit.Trim().ToLower();
                    query = query.Where(p => p.UNITE_GESTIONNAIRE.ToLower().Contains(searchUnit));
                }

                // 3. Filtre par Matricule
                if (!string.IsNullOrEmpty(matricule))
                {
                    query = query.Where(p => p.MATCLE.Contains(matricule.Trim()));
                }

                // 4. Exécution
                var data = await query.ToListAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                // Si ça plante, on veut savoir pourquoi dans la console Visual Studio
                return StatusCode(500, $"Erreur interne: {ex.Message}");
            }
        }
    }
}
