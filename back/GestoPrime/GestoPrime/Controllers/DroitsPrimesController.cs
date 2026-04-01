using GestoPrime.Data;
using GestoPrime.DTOS;
using GestoPrime.model;
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

        // GET: api/DroitsPrimes
        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] string? search)
        {
            var query = _context.UoGestionnaires.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
            {
                // Recherche étendue sur l'unité et le matricule
                query = query.Where(x => x.UniteGestionnaire.Contains(search)
                                      || (x.MAT_RESP != null && x.MAT_RESP.Contains(search)));
            }

            var items = await query.ToListAsync();
            return Ok(new { items, totalCount = items.Count });
        }

        // GET: api/DroitsPrimes/lookup/07011665
        [HttpGet("lookup/{matricule}")]
        public async Task<ActionResult<UoGestionnaire>> GetByMatricule(string matricule)
        {
            var info = await _context.UoGestionnaires
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.MAT_RESP == matricule);

            if (info == null)
            {
                return NotFound(new { message = $"Responsable avec matricule {matricule} introuvable." });
            }

            return Ok(info);
        }

        // PUT: api/DroitsPrimes/update
        // Note: On utilise souvent PUT pour les mises à jour
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] DroitsPrimeDto model)
        {
            if (model == null) return BadRequest("Les données sont manquantes.");

            try
            {
                // Recherche prioritaire par ID si disponible, sinon par Nom d'unité
                var entity = await _context.UoGestionnaires
                    .FirstOrDefaultAsync(u => u.Id == model.Id
                                         || u.UniteGestionnaire == model.Unite_Gestionnaire);

                if (entity == null)
                {
                    return NotFound(new { message = "Unité gestionnaire introuvable dans le paramétrage." });
                }

                // Mise à jour des propriétés
                entity.Droit_Hygiene = model.Droit_Hygiene;
                entity.Droit_Prod = model.Droit_Prod;
                entity.Utilisateur = model.Utilisateur;
                entity.Date_Mvt = DateTime.Now;

                await _context.SaveChangesAsync();
                return Ok(new { message = "Paramétrage mis à jour avec succès." });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { message = "Erreur lors de la mise à jour en base de données.", details = ex.Message });
            }
        }
    }
}