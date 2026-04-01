using GestoPrime.Data;
using GestoPrime.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestoPrime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControlDroitsPrimesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ControlDroitsPrimesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetDroitsPrimes([FromQuery] string searchTerm = "")
        {
            try
            {
                var query = _context.ControlDroitsPrimes.AsNoTracking().AsQueryable();

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    // .Trim() et .ToLower() pour une recherche plus flexible
                    string search = searchTerm.Trim().ToLower();

                    query = query.Where(x =>
                        x.Unite_Gestionnaire.ToLower().Contains(search) ||
                        x.MAT_RESP.ToLower().Contains(search) ||
                        x.NOM_PRENOM_RESP.ToLower().Contains(search)); // Ajout de la recherche par nom
                }

                var data = await query.ToListAsync();
                return Ok(new { items = data });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur SQL", error = ex.Message });
            }
        }
    }
}