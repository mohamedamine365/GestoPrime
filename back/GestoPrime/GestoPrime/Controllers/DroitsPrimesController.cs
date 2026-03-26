using GestoPrime.Data;
using GestoPrime.DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestoPrime.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DroitsPrimesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DroitsPrimesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. GET : Récupère la liste depuis T_PARAM_UNITE_GESTIONNAIRE
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search)
        {
            // On interroge la table physique au lieu de la vue
            var query = _context.UoGestionnaires.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x => x.Unite_Gestionnaire.Contains(search));
            }

            var items = await query.ToListAsync();
            return Ok(new { items, totalCount = items.Count });
        }

        // 2. GET : Recherche par matricule responsable dans la table physique
        [HttpGet("lookup/{matricule}")]
        public async Task<IActionResult> GetByMatricule(string matricule)
        {
            // Recherche basée sur la colonne MAT_RESP de la table T_PARAM_UNITE_GESTIONNAIRE
            var info = await _context.UoGestionnaires
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.MAT_RESP == matricule);

            if (info == null)
            {
                return NotFound(new { message = "Responsable introuvable dans le paramétrage." });
            }

            return Ok(info);
        }

        // 3. POST : Mise à jour des droits dans T_PARAM_UNITE_GESTIONNAIRE
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] DroitsPrimeDto model)
        {
            try
            {
                if (model == null) return BadRequest(new { message = "Données invalides" });

                var entity = await _context.UoGestionnaires
                    .FirstOrDefaultAsync(u => u.Unite_Gestionnaire == model.Unite_Gestionnaire);

                if (entity == null)
                    return NotFound(new { message = "Paramétrage introuvable pour cette unité." });

                entity.Droit_Hygiene = model.Droit_Hygiene;
                entity.Droit_Prod = model.Droit_Prod;
                entity.Date_Mvt = DateTime.Now;

                await _context.SaveChangesAsync();
                return Ok(new { message = "Mise à jour réussie !" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur SQL", details = ex.Message });
            }
        }
    }
}