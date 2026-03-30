using GestoPrime.Data;
using GestoPrime.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestoPrime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalarieController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SalarieController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalarieConsultationDto>>> GetSalaries([FromQuery] string? search)
        {
            try
            {
                var query = _context.V_CONSULTATION_SALARIE.AsNoTracking().AsQueryable();

                // 1. Logique de filtrage (Utilise les noms de ton MODEL ConsultationSalarie)
                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(s =>
                        (s.Matricule != null && s.Matricule.Contains(search)) ||
                        (s.NomPrenom != null && s.NomPrenom.Contains(search)) ||
                        (s.LibelleUnite != null && s.LibelleUnite.Contains(search))
                    );
                }

                // 2. Projection vers le DTO (Mapping propre pour Angular)
                var result = await query.Select(s => new SalarieConsultationDto
                {
                    Periode = s.Periode ?? "",
                    Statut = s.Statut ?? "",
                    UniteGestionnaire = s.UniteGestionnaire ?? "",
                    Matricule = s.Matricule ?? "",
                    NomPrenom = s.NomPrenom ?? "",
                    Etablissement = s.Etablissement ?? "",
                    CodeUnite = s.CodeUnite ?? "",
                    LibelleUnite = s.LibelleUnite ?? "",
                    MatResponsable = s.MatriculeResponsable ?? "",
                    ResponsableInfo = s.NomPrenomResponsable != null
                        ? $"{s.NomPrenomResponsable} ({s.MatriculeResponsable})"
                        : "Non défini"
                }).ToListAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur SQL", detail = ex.Message });
            }
        }
    }
}
