using GestoPrime.Data;
using GestoPrime.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestoPrime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvancePrimeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AvancePrimeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ControleAvancePrimeDto>>> GetAvances([FromQuery] string? recherche)
        {
            try
            {
                // 1. Préparation de la requête sur la vue SQL
                // Source: V_CONTROLE_CALCUL_PRIME_AVANCE_PRIME_RESULTAT
                var query = _context.V_CONTROLE_AVANCE_PRIME.AsNoTracking().AsQueryable();

                // 2. Logique de recherche globale (Matricule ou Nom)
                if (!string.IsNullOrEmpty(recherche))
                {
                    var term = recherche.Trim().ToLower();
                    query = query.Where(p => p.MATRICULE.Contains(term)
                                          || p.NOM_PRENOM.ToLower().Contains(term));
                }

                // 3. Mapping vers le DTO pour le Frontend
                var data = await query.Select(p => new ControleAvancePrimeDto
                {
                    Periode = p.Periode,                   // Colonne SQL: Periode
                    Matricule = p.MATRICULE,               // Colonne SQL: MATRICULE
                    NomPrenom = p.NOM_PRENOM,              // Colonne SQL: NOM_PRENOM
                    Statut = p.STATUT_SALARIE,             // Colonne SQL: STATUT_SALARIE
                    Plafond = p.PLAFOND,                   // Colonne SQL: PLAFOND
                    NbJoursTravaille = p.Nombre_Jour_Traville, // Colonne SQL: Nombre_Jour_Traville
                    NbJoursConge = p.Nombre_Jour_Conge,    // Colonne SQL: Nombre_Jour_Conge
                    NbJoursCongeNonPaye = p.Nombre_Jour_Conge_Non_Paye // Colonne SQL: Nombre_Jour_Conge_Non_Paye
                }).ToListAsync();

                return Ok(data);
            }
            catch (Exception ex)
            {
                // Log de l'erreur interne pour le débogage
                return StatusCode(500, $"Erreur lors de la récupération des avances : {ex.Message}");
            }
        }
    }
}

