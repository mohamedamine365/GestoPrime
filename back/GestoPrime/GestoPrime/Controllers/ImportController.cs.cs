using GestoPrime.Data;
using GestoPrime.model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Globalization;

namespace GestoPrime.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ImportController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ImportController(ApplicationDbContext context) { _context = context; }

        [HttpPost("import-salaries")]
        public async Task<IActionResult> ImportExcel(IFormFile file)
        {
            OfficeOpenXml.ExcelPackage.License.SetNonCommercialPersonal("GestoPrime");

            if (file == null || file.Length == 0) return BadRequest(new { error = "Fichier invalide." });

            var currentUserId = User.FindFirst("Matricule")?.Value ?? "System";
            var list = new List<MoisSalarie>();
            var uniqueMatricules = new HashSet<string>();

            try
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    stream.Position = 0;

                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension?.Rows ?? 0;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            var matricule = worksheet.Cells[row, 1].Value?.ToString()?.Trim();

                            // Éviter les lignes vides ou les doublons de matricule dans le même fichier
                            if (string.IsNullOrEmpty(matricule) || uniqueMatricules.Contains(matricule)) continue;
                            uniqueMatricules.Add(matricule);

                            // --- GESTION DU FORMAT DATE 01/01/2025 00:00:00 ---
                            DateTime parseDate(object cellValue, DateTime defaultDate)
                            {
                                if (cellValue is DateTime dt) return dt; // Déjà une date Excel
                                if (DateTime.TryParse(cellValue?.ToString(), out DateTime result)) return result;
                                return defaultDate;
                            }

                            var debut = parseDate(worksheet.Cells[row, 2].Value, DateTime.Now);
                            var fin = parseDate(worksheet.Cells[row, 3].Value, new DateTime(9999, 12, 31));

                            list.Add(new MoisSalarie
                            {
                                MATRICULE = matricule,
                                // On s'assure que l'heure est bien présente (par défaut 00:00:00)
                                DEBUT = debut,
                                Date_FIN = fin,

                                NOM_PREMON = worksheet.Cells[row, 4].Value?.ToString(),
                                COD_GROUPE = worksheet.Cells[row, 5].Value?.ToString(),
                                LIBELLE_GROUPE = worksheet.Cells[row, 6].Value?.ToString(),
                                COD_POLE = worksheet.Cells[row, 7].Value?.ToString(),
                                LIBELLE_POLE = worksheet.Cells[row, 8].Value?.ToString(),
                                COD_SITE = worksheet.Cells[row, 9].Value?.ToString(),
                                LIBELLE_SITE = worksheet.Cells[row, 10].Value?.ToString(),
                                COD_ETAB = worksheet.Cells[row, 11].Value?.ToString(),
                                LIBELLE_ETAB = worksheet.Cells[row, 12].Value?.ToString(),
                                MATRICULE_RESP = worksheet.Cells[row, 13].Value?.ToString(),
                                NOM_PRENOM_RESP = worksheet.Cells[row, 14].Value?.ToString(),
                                COMPTE_LOTUS_RESP = worksheet.Cells[row, 15].Value?.ToString(),
                                COD_UNITE = worksheet.Cells[row, 16].Value?.ToString(),
                                LIBELLE_UNITE = worksheet.Cells[row, 17].Value?.ToString(),
                                UNITE_GEST = worksheet.Cells[row, 18].Value?.ToString(),
                                TYPE_CONTRAT = worksheet.Cells[row, 19].Value?.ToString(),
                                STATUT = worksheet.Cells[row, 20].Value?.ToString(),
                                Matricule_User = currentUserId,
                                Date_Mvt = DateTime.Now
                            });
                        }
                    }
                }

                if (list.Any())
                {
                    // Optionnel : Supprimer les doublons existants en base avant l'ajout si nécessaire
                    // var matricules = list.Select(l => l.MATRICULE).ToList();
                    // var existing = _context.MoisSalaries.Where(x => matricules.Contains(x.MATRICULE));
                    // _context.MoisSalaries.RemoveRange(existing);

                    _context.AddRange(list);
                    await _context.SaveChangesAsync();
                }

                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = ex.InnerException?.Message ?? ex.Message,
                    details = "Erreur SQL lors de l'insertion. Vérifiez si le matricule existe déjà."
                });
            }
        }


        [HttpPost("import-score")]
        public async Task<IActionResult> ImportScore(IFormFile file)
        {
            OfficeOpenXml.ExcelPackage.License.SetNonCommercialPersonal("GestoPrime");
            if (file == null || file.Length == 0) return BadRequest(new { error = "Fichier Excel vide ou invalide." });

            // Récupération de l'utilisateur connecté pour l'audit
            var currentUserId = User.FindFirst("Matricule")?.Value ?? "System";

            // Utilisation d'un dictionnaire pour éviter les doublons de matricule dans le même fichier
            var scoresDict = new Dictionary<string, MoisScoreBrut>();

            try
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension?.Rows ?? 0;

                        for (int row = 2; row <= rowCount; row++) // On commence à 2 pour sauter l'entête
                        {
                            var matricule = worksheet.Cells[row, 3].Value?.ToString()?.Trim();
                            if (string.IsNullOrEmpty(matricule)) continue;

                            // Lecture sécurisée des nombres
                            int.TryParse(worksheet.Cells[row, 1].Value?.ToString(), out int annee);
                            int.TryParse(worksheet.Cells[row, 2].Value?.ToString(), out int mois);

                            // Gestion de la virgule/point pour la note (18,5)
                            string noteStr = worksheet.Cells[row, 8].Value?.ToString()?.Replace(',', '.');
                            decimal.TryParse(noteStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal note);

                            var score = new MoisScoreBrut
                            {
                                ANNEE = annee,
                                MOIS = mois,
                                MATRICULE = matricule,
                                NOM_PREMON = worksheet.Cells[row, 4].Value?.ToString()?.Trim(),
                                SERVICE = worksheet.Cells[row, 5].Value?.ToString()?.Trim(),
                                DEMANDE = worksheet.Cells[row, 6].Value?.ToString()?.Trim(),
                                OBTENU = worksheet.Cells[row, 7].Value?.ToString()?.Trim(),
                                NOTE = note,
                                ETABLISSEMENT = worksheet.Cells[row, 9].Value?.ToString()?.Trim(),
                                IS_NOTE = worksheet.Cells[row, 10].Value?.ToString()?.Trim(),
                                Matricule_User = currentUserId,
                                Date_Mvt = DateTime.Now
                            };

                            scoresDict[matricule] = score; // Écrase si doublon dans le Excel
                        }
                    }
                }

                if (scoresDict.Any())
                {
                    var finalScoresList = scoresDict.Values.ToList();
                    var ids = finalScoresList.Select(s => s.MATRICULE).ToList();

                    // 1. Suppression des anciens scores pour ces matricules (évite PK violation)
                    var existing = _context.Set<MoisScoreBrut>().Where(x => ids.Contains(x.MATRICULE));
                    _context.Set<MoisScoreBrut>().RemoveRange(existing);
                    await _context.SaveChangesAsync();

                    // 2. Insertion des nouveaux scores
                    _context.Set<MoisScoreBrut>().AddRange(finalScoresList);
                    await _context.SaveChangesAsync();

                    return Ok(finalScoresList);
                }

                return BadRequest(new { error = "Aucune donnée valide trouvée dans le fichier." });
            }
            catch (Exception ex)
            {
                // On renvoie l'InnerException car c'est là que SQL Server dit pourquoi il a refusé
                var detailedError = ex.InnerException?.Message ?? ex.Message;
                return StatusCode(500, new { error = detailedError });
            }
        }
    }
}