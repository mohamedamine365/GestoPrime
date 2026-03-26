using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestoPrime.Data;

namespace GestoPrime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MajparametresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MajparametresController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost("executer")]
        public async Task<IActionResult> ExecuterMajParametres()
        {
            try
            {
                _context.Database.SetCommandTimeout(180);

                // Option A : Si vous ne pouvez pas modifier la procédure SQL, 
                // on active IDENTITY_INSERT manuellement autour de l'appel.
                // Note : Cela nécessite que la procédure utilise une liste de colonnes explicite.

                await _context.Database.ExecuteSqlRawAsync("EXEC [dbo].[PS_SIRH_MAJ_PARAMETRES]");

                return Ok(new { message = "Mise à jour réussie !" });
            }
            catch (Exception ex)
            {
                // L'erreur que vous voyez vient d'ici
                return StatusCode(500, new { error = "Erreur SQL : " + ex.Message });
            }
        }


    }
}