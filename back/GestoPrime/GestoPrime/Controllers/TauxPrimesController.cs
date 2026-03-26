using GestoPrime.Data;
using GestoPrime.DTOS;
using GestoPrime.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestoPrime.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TauxPrimesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TauxPrimesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TauxPrimes?searchTerm=...
        [HttpGet]
        public async Task<IActionResult> GetTauxPrimes([FromQuery] string searchTerm = "")
        {
            var items = await _context.TauxPrimes
                .Where(t => string.IsNullOrEmpty(searchTerm) || t.Unite_Gestionnaire.Contains(searchTerm))
                .Select(t => new TauxPrimeDto
                {
                    Id = t.id,
                    Unite_Gestionnaire = t.Unite_Gestionnaire,
                    NbrJourOuv = t.NBR_JOUR_OUV,
                    CoefHygiene = t.Cof_HYGIENE,
                    CoefProd = t.Cof_PROD,
                    Periode = t.Periode
                })
                .ToListAsync();

            return Ok(new { items, total = items.Count });
        }

        // PUT: api/TauxPrimes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaux(int id, [FromBody] TauxPrimeDto updatedData)
        {
            var taux = await _context.TauxPrimes.FirstOrDefaultAsync(x => x.id == id);

            if (taux == null) return NotFound(new { message = "Ligne non trouvée" });

            // Récupération de l'utilisateur pour la traçabilité
            var userMatricule = User.FindFirst("Matricule")?.Value ?? "System";

            taux.Cof_HYGIENE = updatedData.CoefHygiene;
            taux.Cof_PROD = updatedData.CoefProd;
            taux.NBR_JOUR_OUV = updatedData.NbrJourOuv;
            taux.Date_Mvt = DateTime.Now;
            taux.Utilisateur = userMatricule;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Mise à jour réussie" });
        }
    }
}