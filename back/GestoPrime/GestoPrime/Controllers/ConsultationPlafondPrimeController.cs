using GestoPrime.Data;
using GestoPrime.Dtos;
using GestoPrime.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GestoPrime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultationPlafondPrimeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ConsultationPlafondPrimeController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string searchTerm = "")
        {
            try
            {
                var query = _context.ConsultationPlafondPrimeRendements.AsNoTracking().AsQueryable();

                // Filtrage
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    string s = searchTerm.Trim().ToLower();
                    query = query.Where(x => x.MATRICULE.Contains(s) || x.Nom_Prenom.ToLower().Contains(s));
                }

                // Récupération des données brutes
                var rows = await query.ToListAsync();

                // Transformation en DTO avec formatage
                var data = rows.Select(x => new ConsultationPlafondPrimeDto
                {
                    Periode = x.Periode,
                    Statut = x.Statut,
                    UniteGestionnaire = x.UNITE_GESTIONNAIRE,
                    Matricule = x.MATRICULE,
                    NomPrenom = x.Nom_Prenom,
                    MatriculeResp = x.MATRICULE_RESP,
                    NomPrenomResp = x.NOM_PRENOM_RESP,

                    // On mappe les colonnes SQL (x.DATE_DEBUT) vers le DTO (DateDebut)
                    DateDebut = x.DATE_DEBUT ?? "---",
                    DateFin = x.DATE_FIN ?? "---",

                    CodeRubrique = x.Code_Rubrique ?? "",
                    Montant = x.MONTANT ?? 0m
                }).ToList();

                return Ok(new { items = data, total = data.Count });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur", error = ex.Message });
            }
        }
    }
}
