using GestoPrime.Data;
using GestoPrime.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestoPrime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultationPlafondIndemniteController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ConsultationPlafondIndemniteController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsultationPlafondIndemniteDto>>> GetPlafonds([FromQuery] string? search)
        {
            try
            {
                var query = _context.ConsultationPlafondIndemniteDeplacements.AsNoTracking().AsQueryable();

                // Filtrage dynamique (Matricule ou Nom)
                if (!string.IsNullOrEmpty(search))
                {
                    var searchLower = search.ToLower();
                    query = query.Where(p =>
                        p.Matricule.ToLower().Contains(searchLower) ||
                        (p.NomPrenom != null && p.NomPrenom.ToLower().Contains(searchLower))
                    );
                }

                var result = await query.Select(p => new ConsultationPlafondIndemniteDto
                {
                    Matricule = p.Matricule,
                    NomPrenom = p.NomPrenom ?? "Inconnu",
                    DateDebut = p.Debut.HasValue ? p.Debut.Value.ToString("dd/MM/yyyy") : "---",
                    DateFin = p.Fin.HasValue ? p.Fin.Value.ToString("dd/MM/yyyy") : "---",
                    CodeRubrique = p.CodRub ?? "",
                    Montant = p.Montant ?? 0
                }).ToListAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur lors de l'accès à la vue SQL", detail = ex.Message });
            }
        }
    }
}
