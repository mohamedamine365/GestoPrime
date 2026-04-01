using GestoPrime.Data;
using GestoPrime.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestoPrime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultationUniteGestionnaireController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ConsultationUniteGestionnaireController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsultationUniteGestionnaireDto>>> GetUnites([FromQuery] string? search)
        {
            try
            {
                var query = _context.ConsultationUniteGestionnaires.AsNoTracking().AsQueryable();

                if (!string.IsNullOrEmpty(search))
                {
                    var s = search.ToLower().Trim();
                    query = query.Where(u =>
                        (u.UniteGestionnaire != null && u.UniteGestionnaire.ToLower().Contains(s)) ||
                        (u.CodeUnite != null && u.CodeUnite.ToLower().Contains(s)) ||
                        (u.LibelleUnite != null && u.LibelleUnite.ToLower().Contains(s))
                    );
                }

                var result = await query.Select(u => new ConsultationUniteGestionnaireDto
                {
                    UniteGestionnaire = u.UniteGestionnaire != null ? u.UniteGestionnaire.Trim() : "---",
                    CodeUnite = u.CodeUnite != null ? u.CodeUnite.Trim() : "---",
                    NomPrenom = u.LibelleUnite != null ? u.LibelleUnite.Trim() : "---"
                }).ToListAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur accès Unité Gestionnaire", detail = ex.Message });
            }
        }
    }
}
