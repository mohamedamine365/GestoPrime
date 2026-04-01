using System.ComponentModel.DataAnnotations.Schema;

namespace GestoPrime.Models
{
    [Table("V_CONSULTATION_PLAFOND_PRIME")]
    public class ConsultationPlafondPrimeRendement
    {
        public string? Periode { get; set; }
        public string? Statut { get; set; }

        [Column("UNITE_GESTIONNAIRE")]
        public string? UNITE_GESTIONNAIRE { get; set; }

        public string MATRICULE { get; set; } = string.Empty;

        [Column("NOM_PRENOM")]
        public string? Nom_Prenom { get; set; }

        public string? MATRICULE_RESP { get; set; }
        public string? NOM_PRENOM_RESP { get; set; }

        // Si le cast échoue, c'est que SQL envoie peut-être déjà du texte.
        // On utilise 'object' pour laisser EF décider, ou on repasse en 'DateTime?' 
        // en vérifiant bien que la colonne SQL est de type date.
        // Changez DateTime? par string? si la colonne SQL est un VARCHAR
        [Column("DATE_DEBUT")]
        public string? DATE_DEBUT { get; set; }

        [Column("DATE_FIN")]
        public string? DATE_FIN { get; set; }

        [Column("COD_RUB")]
        public string? Code_Rubrique { get; set; }

        [Column("MONTANT")]
        public decimal? MONTANT { get; set; }
    }
}