using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestoPrime.model
{
    [Table("T_INT_MOIS_SCORE_PPM_BRUT")]
    public class MoisScoreBrut
    {
        [Key] // Comme demandé, le matricule sert d'ID
        public string MATRICULE { get; set; } = string.Empty;

        public int? ANNEE { get; set; }
        public int? MOIS { get; set; }
        public string? NOM_PREMON { get; set; }
        public string? SERVICE { get; set; }
        public string? DEMANDE { get; set; }
        public string? OBTENU { get; set; }
        public decimal? NOTE { get; set; }
        public string? ETABLISSEMENT { get; set; }
        public string? IS_NOTE { get; set; }

        public string? Matricule_User { get; set; }

        // Format demandé : 01/01/2025 00:00:00
        public DateTime? Date_Mvt { get; set; }
    }
}