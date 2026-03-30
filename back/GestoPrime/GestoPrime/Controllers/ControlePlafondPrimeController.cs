using GestoPrime.Data;
using GestoPrime.DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ControlePlafondPrimeController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ControlePlafondPrimeController(ApplicationDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ControlePlafondPrimeDto>>> GetPlafonds([FromQuery] string? recherche)
    {
        try
        {
            // 1. On commence par la requête de base
            var query = _context.ControlePlafondPrimes.AsQueryable();

            // 2. Application du filtre global
            if (!string.IsNullOrWhiteSpace(recherche))
            {
                // On passe tout en minuscule pour ignorer la casse (Case Insensitive)
                string searchLower = recherche.ToLower().Trim();

                query = query.Where(x =>
                    (x.NomPrenom != null && x.NomPrenom.ToLower().Contains(searchLower)) ||
                    (x.Matricule != null && x.Matricule.ToLower().Contains(searchLower)) ||
                    (x.UniteGestionnaire != null && x.UniteGestionnaire.ToLower().Contains(searchLower))
                );
            }

            // 3. Projection vers le DTO
            var result = await query
                .Select(p => new ControlePlafondPrimeDto
                {
                    Periode = p.Periode ?? "",
                    Statut = p.Statut ?? "",
                    UniteGestionnaire = p.UniteGestionnaire ?? "",
                    Matricule = p.Matricule ?? "",
                    NomPrenom = p.NomPrenom ?? "",
                    MatriculeResp = p.MatriculeResp ?? "",
                    NomPrenomResp = p.NomPrenomResp ?? "",
                    DateDebut = p.DateDebut,
                    DateFin = p.DateFin,
                    CodeRubrique = p.CodRub ?? "",
                    Montant = p.Montant ?? 0
                })
                .ToListAsync();

            return Ok(result);
        }
        catch (Exception ex)
        {
            // Loggez l'erreur ici si vous avez un logger
            return StatusCode(500, $"Erreur interne: {ex.Message}");
        }
    }
}