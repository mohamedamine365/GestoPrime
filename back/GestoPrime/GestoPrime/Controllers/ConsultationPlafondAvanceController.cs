using GestoPrime.Data;
using GestoPrime.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestoPrime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultationPlafondAvanceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ConsultationPlafondAvanceController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsultationPlafondAvanceDto>>> GetPlafonds([FromQuery] string? search)
        {
            try
            {
                var query = _context.ConsultationPlafondAvances.AsNoTracking().AsQueryable();

                if (!string.IsNullOrEmpty(search))
                {
                    var sLower = search.ToLower().Trim();
                    query = query.Where(p =>
                        p.Matricule.ToLower().Contains(sLower) ||
                        (p.NomPrenom != null && p.NomPrenom.ToLower().Contains(sLower))
                    );
                }

                var result = await query.Select(p => new ConsultationPlafondAvanceDto
                {
                    Matricule = p.Matricule,
                    NomPrenom = p.NomPrenom ?? "---",
                    // On passe directement la string car le cast SQL est déjà fait
                    DateDebut = p.Debut ?? "---",
                    DateFin = p.Fin ?? "---",
                    CodeRubrique = p.CodRub ?? "",
                    Montant = p.Montant ?? 0
                }).ToListAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur de mapping", detail = ex.Message });
            }
        }
    }
}

