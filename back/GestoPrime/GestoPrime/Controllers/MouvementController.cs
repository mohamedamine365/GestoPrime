using GestoPrime.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class MouvementController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public MouvementController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? search,
        [FromQuery] string? resultat,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        // On utilise AsNoTracking() pour améliorer les performances (lecture seule)
        var query = _context.Mouvements.AsNoTracking().AsQueryable();

        // 1. RECHERCHE GLOBALE (Insensible à la casse)
        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim().ToLower();
            query = query.Where(m => (m.Matricule != null && m.Matricule.Contains(search)) ||
                                     (m.NomPrenom != null && m.NomPrenom.ToLower().Contains(search)));
        }

        // 2. FILTRE STATUT
        if (!string.IsNullOrWhiteSpace(resultat))
        {
            query = query.Where(m => m.Resultat == resultat);
        }

        // 3. TOTAL COUNT
        var totalCount = await query.CountAsync();

        // 4. RÉCUPÉRATION PAGINÉE
        // On s'assure que page et pageSize sont valides pour éviter les erreurs SQL
        int validPageSize = pageSize > 0 ? pageSize : 10;
        int validPage = page > 0 ? page : 1;

        var items = await query
            .OrderByDescending(m => m.Date_Mvt)
            .Skip((validPage - 1) * validPageSize)
            .Take(validPageSize)
            .ToListAsync();

        return Ok(new { items, totalCount });
    }
}