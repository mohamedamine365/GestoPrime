using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestoPrime.Models
{
    [Table("V_MAJ_PLAFOND_PRIME_INDEMNITE_DEPLACEMENT")]
    public class ConsultationPlafondIndemniteDeplacement
    {
        [Column("MATRICULE")]
        public string Matricule { get; set; } = string.Empty;

        [Column("NOM_PRENOM")]
        public string? NomPrenom { get; set; }

        [Column("DEBUT")]
        public DateTime? Debut { get; set; }

        [Column("FIN")]
        public DateTime? Fin { get; set; }

        [Column("COD_RUB")]
        public string? CodRub { get; set; }

        [Column("MONTANT")]
        public decimal? Montant { get; set; }
    }
}