using GestoPrime.Data;
using GestoPrime.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestoPrime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndemniteDeplacementController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public IndemniteDeplacementController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IndemniteDeplacementDto>>> GetIndemnites([FromQuery] string? recherche)
        {
            try
            {
                // 1. Appel de la vue mappée dans le DbContext
                var query = _context.IndemnitesDeplacement.AsQueryable();

                // 2. Filtrage global (Matricule ou Nom)
                if (!string.IsNullOrWhiteSpace(recherche))
                {
                    string searchLower = recherche.ToLower().Trim();
                    query = query.Where(x =>
                        (x.MATRICULE != null && x.MATRICULE.Contains(searchLower)) ||
                        (x.NOM_PRENOM != null && x.NOM_PRENOM.ToLower().Contains(searchLower))
                    );
                }

                // 3. Projection vers le DTO pour le Frontend
                var result = await query
                    .Select(x => new IndemniteDeplacementDto
                    {
                        Periode = x.Periode ?? "",
                        Matricule = x.MATRICULE ?? "",
                        NomPrenom = x.NOM_PRENOM ?? "",
                        StatutSalarie = x.STATUT_SALARIE ?? "",
                        CodeRubrique = x.COD_RUB ?? "",
                        Plafond = x.PLAFOND ?? 0,
                        NbrJourTrav = x.NBR_JOUR_TRAV ?? 0,
                        NbrJourCongePaye = x.NBR_JOUR_CONGE_PAYE ?? 0,
                        NbrCongeNonPaye = x.NBR_CONGE_NON_PAYE ?? 0,
                        NbrCongePrime = x.NBR_CONGE_PRIME ?? 0
                    })
                    .ToListAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur lors de la récupération des indemnités : {ex.Message}");
            }
        }
    }

}
