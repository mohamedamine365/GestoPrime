using GestoPrime.Data;
using GestoPrime.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

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
                var periodes = await _context.Periodes
                    .OrderByDescending(p => p.id)
                    .ToListAsync();
                return Ok(periodes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erreur de lecture : " + ex.Message });
            }
        }

        [HttpPost("lancer-periode")] // Changé de HttpPatch à HttpPost
        public async Task<IActionResult> LancerPeriode([FromBody] PeriodeRequest request)
        {
            // 1. Audit : Matricule de l'utilisateur
            var currentUserId = User.FindFirst("Matricule")?.Value
                                ?? User.Identity?.Name;
            try
            {
                // 2. Validation Format (Ex: MT202603)
                if (string.IsNullOrEmpty(request.PeriodeCode) || !Regex.IsMatch(request.PeriodeCode, @"^MT\d{6}$"))
                {
                    return BadRequest(new { message = "Format de code période invalide (Ex: MT202603)." });
                }
                // 4. Sécurité Timeout (5 minutes)
                _context.Database.SetCommandTimeout(300);

                // 5. Exécution de la procédure avec les 3 paramètres demandés
                // La procédure s'occupe de l'UPDATE de T_EXP_UO_GESTIONNAIRE et HistoriqueMvtGrh
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC [dbo].[PS_SIRH_INSERTION_PERIODE] @p0, @p1, @p2",
                    request.PeriodeCode,
                    "Ouverte",
                    currentUserId
                );

                return Ok(new
                {
                    success = true,
                    message = $"Période {request.PeriodeCode} créée et lancée avec succès.",
                    user = currentUserId
                });
            }
            catch (Exception ex)
            {
                _context.Database.SetCommandTimeout(30);
                return StatusCode(500, new
                {
                    error = "Erreur : " + ex.Message,
                    details = "Vérifiez que la procédure SQL a été mise à jour sans les liens serveurs 212."
                });
            }
        }
    }

    public class PeriodeRequest { public string PeriodeCode { get; set; } }
}