using GestoPrime.Data;
using GestoPrime.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestoPrime.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeriodeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PeriodeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPeriodes()
        {
            try
            {
                // Récupère toutes les périodes de la table T_INT_PERIODE
                var periodes = await _context.Periodes
                    .OrderByDescending(p => p.id)
                    .ToListAsync();

                return Ok(periodes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }





        [HttpPost("lancer")]
        public async Task<IActionResult> LancerPeriode([FromBody] PeriodeRequest request)
        {
            // Récupération du matricule de l'utilisateur connecté
            var currentUserId = User.FindFirst("Matricule")?.Value ?? "System";

            try
            {
                // 1. Vérifier si cette période existe déjà
                var dejaExistant = await _context.Periodes
                    .AnyAsync(p => p.Periode_Val == request.PeriodeCode);

                if (dejaExistant)
                    return BadRequest(new { message = "Cette période est déjà enregistrée." });

                // 2. Clôturer automatiquement toute période encore "Ouverte"
                var periodesOuvertes = await _context.Periodes
                    .Where(p => p.Statut == "Ouverte")
                    .ToListAsync();

                foreach (var p in periodesOuvertes) { p.Statut = "Clôturée"; }

                // 3. Créer la nouvelle période
                var nouvellePeriode = new Periode
                {
                    Periode_Val = request.PeriodeCode,
                    Statut = "Ouverte",
                    Utilisateur = currentUserId,
                    Date_Mvt = DateTime.Now
                };

                _context.Periodes.Add(nouvellePeriode);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Période lancée avec succès !" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }

    // Petit DTO local pour recevoir la requête
    public class PeriodeRequest { public string PeriodeCode { get; set; } }
}