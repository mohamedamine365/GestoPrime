using System.ComponentModel.DataAnnotations.Schema;

namespace GestoPrime.Models
{
    [Table("V_MAJ_PLAFOND_PRIME_AVANCE_PRIME_RESULTAT")]
    public class ConsultationPlafondAvance
    {
        [Column("MATRICULE")]
        public string Matricule { get; set; } = string.Empty;

        [Column("NOM_PRENOM")]
        public string? NomPrenom { get; set; }

        // Changement de type : DateTime? -> string
        [Column("DEBUT")]
        public string? Debut { get; set; }

        [Column("FIN")]
        public string? Fin { get; set; }

        [Column("COD_RUB")]
        public string? CodRub { get; set; }

        [Column("MONTANT")]
        public decimal? Montant { get; set; }
    }
}