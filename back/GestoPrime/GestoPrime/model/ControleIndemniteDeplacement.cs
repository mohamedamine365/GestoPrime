using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestoPrime.model
{
    [Table("V_CONTROLE_CALCUL_PRIME_INDEMNITE_DEPLACEMENT")]
    public class ControleIndemniteDeplacement
    {
        public string? Periode { get; set; }

        [Key] // Nécessaire pour EF Core même sur une vue
        public string MATRICULE { get; set; } = null!;

        public string? NOM_PRENOM { get; set; }
        public string? STATUT_SALARIE { get; set; }
        public string? COD_RUB { get; set; }

        [Column(TypeName = "decimal(18, 3)")]
        public decimal? PLAFOND { get; set; }

        public double? NBR_JOUR_TRAV { get; set; }
        public double? NBR_JOUR_CONGE_PAYE { get; set; }
        public double? NBR_CONGE_NON_PAYE { get; set; }
        public double? NBR_CONGE_PRIME { get; set; }
    
     }
}
