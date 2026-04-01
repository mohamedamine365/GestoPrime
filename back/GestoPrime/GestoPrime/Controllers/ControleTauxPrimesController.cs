using GestoPrime.Data;
using GestoPrime.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestoPrime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControleTauxPrimesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ControleTauxPrimesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTauxPrimes([FromQuery] string searchTerm = "")
        {
            try
            {
                // 1. Lecture de la vue SQL en mode performance (sans suivi de modifications)
                var query = _context.ControleTauxPrimes.AsNoTracking().AsQueryable();

                // 2. Logique de recherche par Période ou Unité Gestionnaire
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    string search = searchTerm.Trim().ToLower();
                    query = query.Where(x =>
                        (x.Unite_Gestionnaire != null && x.Unite_Gestionnaire.ToLower().Contains(search)) ||
                        x.Periode.ToLower().Contains(search));
                }

                // 3. Projection vers le DTO (Mapping explicite)
                var data = await query.Select(x => new ControleTauxPrimesDto
                {
                    Periode = x.Periode,
                    // Retourne la valeur réelle ou null si absente en base
                    UniteGestionnaire = x.Unite_Gestionnaire,
                    NombreJoursOuvrables = x.NBR_JOUR_OUV,
                    CoefHygiene = x.Cof_HYGIENE,
                    CoefProductivite = x.COF_PROD,
                    // Récupère le statut exact de la vue SQL (ex: "Ouverte")
                    Statut = x.Statut
                }).ToListAsync();

                // 4. Retour au format attendu par votre interface Angular
                return Ok(new
                {
                    items = data,
                    total = data.Count
                });
            }
            catch (Exception ex)
            {
                // Capture des erreurs de conversion ou de connexion SQL
                return StatusCode(500, new
                {
                    message = "Erreur lors de la récupération des taux de primes",
                    error = ex.Message
                });
            }
        }
    }
}
   