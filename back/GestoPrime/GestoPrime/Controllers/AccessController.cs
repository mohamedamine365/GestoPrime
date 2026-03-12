using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestoPrime.Data;
using GestoPrime.model;
using Microsoft.AspNetCore.Authorization;

namespace GestoPrime.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class AccessController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AccessController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Access (Liste avec pagination)
        [HttpGet]
        public async Task<IActionResult> GetUsers(
     [FromQuery] string? search,
     [FromQuery] int page = 1,
     [FromQuery] int pageSize = 10)
        {
            try
            {
                var query = _context.Users.AsQueryable();

                if (!string.IsNullOrEmpty(search))
                {
                    // Correction : On vérifie si la colonne est nulle avant de faire le .Contains()
                    query = query.Where(u =>
                        (u.NomPrenom != null && u.NomPrenom.Contains(search)) ||
                        (u.Matricule != null && u.Matricule.Contains(search)));
                }

                var totalCount = await query.CountAsync();

                var items = await query
                    .OrderBy(u => u.NomPrenom ?? "") // Protection contre le tri sur du null
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return Ok(new { items, totalCount });
            }
            catch (Exception ex)
            {
                // On renvoie un objet JSON propre pour Angular
                return StatusCode(500, new { message = $"Erreur SQL: {ex.Message}" });
            }
        }

        // POST: api/Access (Ajouter un accès)
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            try
            {
                // 1. Vérifier si le matricule existe dans le référentiel Salariés
                var salarie = await _context.Salaries
                    .FirstOrDefaultAsync(s => s.Matricule == user.Matricule);

                if (salarie == null)
                {
                    return BadRequest(new { message = "Erreur : Ce matricule n'existe pas dans la base du personnel." });
                }

                // 2. Vérifier si l'accès existe déjà pour éviter les doublons (clé primaire)
                var existingAccess = await _context.Users.AnyAsync(u => u.Matricule == user.Matricule);
                if (existingAccess)
                {
                    return BadRequest(new { message = "Cet utilisateur possède déjà un accès." });
                }

                // 3. Compléter les données manquantes
                user.NomPrenom = salarie.NomPrenom;
                user.Etablissement = salarie.Etablissement;
                user.Date_Mvt = DateTime.Now;

                // Si 'Utilisateur' est requis en BDD mais vide dans le JSON
                if (string.IsNullOrEmpty(user.Utilisateur))
                {
                    user.Utilisateur = User.Identity?.Name ?? "Admin_System";
                }

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetUsers), new { id = user.Matricule }, user);
            }
            catch (Exception ex)
            {
                // Retourne l'erreur interne exacte pour le débogage
                return StatusCode(500, new { message = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("check/{matricule}")]
        public async Task<IActionResult> CheckSalarie(string matricule)
        {
            var salarie = await _context.Salaries
                .FirstOrDefaultAsync(s => s.Matricule == matricule);

            if (salarie == null) return NotFound();

            return Ok(new
            {
                nomPrenom = salarie.NomPrenom,
                etablissement = salarie.Etablissement
            });
        }

        [HttpDelete("{matricule}")]
        public async Task<IActionResult> DeleteUser(string matricule)
        {
            var user = await _context.Users.FindAsync(matricule);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}