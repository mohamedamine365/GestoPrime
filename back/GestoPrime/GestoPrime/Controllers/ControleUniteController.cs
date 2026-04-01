using GestoPrime.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestoPrime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControleUniteController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ControleUniteController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? recherche)
        {
            try
            {
                // On prépare la requête sur la vue
                var query = _context.V_CONTROLE_UNITE_GESTIONNAIRE.AsNoTracking().AsQueryable();

                // Si une recherche est saisie, on filtre sur plusieurs colonnes
                if (!string.IsNullOrWhiteSpace(recherche))
                {
                    // .ToLower() et EF Core gèrent généralement l'insensibilité à la casse selon la collation SQL, 
                    // mais Contains est parfait ici pour une recherche globale.
                    query = query.Where(x => (x.MATRICULE != null && x.MATRICULE.Contains(recherche))
                                          || (x.NOM_PRENOM != null && x.NOM_PRENOM.Contains(recherche))
                                          || (x.UNITE_GESTIONNAIRE != null && x.UNITE_GESTIONNAIRE.Contains(recherche)));
                }

                var data = await query.ToListAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                // Log de l'erreur (optionnel) et retour 500
                return StatusCode(500, $"Erreur interne : {ex.Message}");
            }
        }
    }
}