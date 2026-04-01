using GestoPrime.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestoPrime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControlePointageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ControlePointageController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? recherche)
        {
            try
            {
                var query = _context.V_CONTROLE_POINTAGE.AsNoTracking().AsQueryable();

                if (!string.IsNullOrWhiteSpace(recherche))
                {
                    query = query.Where(x =>
                        (x.MATCLE != null && x.MATCLE.Contains(recherche)) ||
                        (x.NMPRES != null && x.NMPRES.Contains(recherche)) ||
                        (x.UNITE_GESTIONNAIRE != null && x.UNITE_GESTIONNAIRE.Contains(recherche))
                    );
                }

                var data = await query.ToListAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur : {ex.Message}");
            }
        }
    }
}
