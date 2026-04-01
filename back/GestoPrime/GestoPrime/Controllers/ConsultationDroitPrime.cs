using GestoPrime.Data;
using GestoPrime.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestoPrime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultationDroitPrime : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ConsultationDroitPrime(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsultationDroitPrimeDto>>> GetDroits([FromQuery] string? search)
        {
            try
            {
                var query = _context.ConsultationDroitPrimes.AsNoTracking().AsQueryable();

                if (!string.IsNullOrEmpty(search))
                {
                    var s = search.ToLower().Trim();
                    query = query.Where(d =>
                        (d.UniteGestionnaire != null && d.UniteGestionnaire.ToLower().Contains(s)) ||
                        (d.MatResp != null && d.MatResp.ToLower().Contains(s)) ||
                        (d.NomPrenomResp != null && d.NomPrenomResp.ToLower().Contains(s))
                    );
                }

                var result = await query.Select(d => new ConsultationDroitPrimeDto
                {
                    UniteGestionnaire = d.UniteGestionnaire != null ? d.UniteGestionnaire.Trim() : "---",
                    MatriculeResp = d.MatResp != null ? d.MatResp.Trim() : "---",
                    NomPrenomResp = d.NomPrenomResp != null ? d.NomPrenomResp.Trim() : "---",
                    DroitHygiene = d.DroitHygiene == 1,
                    DroitProductivite = d.DroitProd == 1
                }).ToListAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur accès Droits Primes", detail = ex.Message });
            }
        }
    }
}
