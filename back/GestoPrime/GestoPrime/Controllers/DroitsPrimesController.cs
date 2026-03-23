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

        // 1. GET : Récupère les données depuis la VUE SQL
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search)
        {
            var query = _context.DroitsPrimes.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x => x.Unite_Gestionnaire.Contains(search));
            }

            var items = await query.ToListAsync();
            return Ok(new { items, totalCount = items.Count });
        }

        [HttpGet("lookup/{matricule}")]
        public async Task<IActionResult> GetByMatricule(string matricule)
        {
            // On cherche dans la VUE car elle contient déjà la jointure Nom/Prénom
            var info = await _context.DroitsPrimes
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.MAT_RESP == matricule);

            if (info == null)
            {
                return NotFound(new { message = "Matricule introuvable" });
            }

            return Ok(info);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] DroitsPrimeDto model)
        {
            try
            {
                if (model == null) return BadRequest(new { message = "Données invalides" });

                // Recherche dans la table de paramétrage
                var entity = await _context.UoGestionnaires
                    .FirstOrDefaultAsync(u => u.Unite_Gestionnaire == model.Unite_Gestionnaire);

                if (entity == null)
                    return NotFound(new { message = "Paramétrage introuvable pour cette unité." });

                // Mise à jour des droits
                entity.Droit_Hygiene = model.Droit_Hygiene;
                entity.Droit_Prod = model.Droit_Prod;
                entity.Date_Mvt = DateTime.Now; // Optionnel : mettre à jour la date de mouvement

                await _context.SaveChangesAsync();

                return Ok(new { message = "Mise à jour réussie !" });
            }
            catch (Exception ex)
            {
                // Renvoie un JSON propre pour éviter l'erreur de parsing Angular
                return StatusCode(500, new { message = "Erreur SQL", details = ex.Message });
            }
        }
    }
}