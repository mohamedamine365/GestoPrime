using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestoPrime.model
{
    // On pointe vers la table qui contient réellement les colonnes de droits
    [Table("T_PARAM_UNITE_GESTIONNAIRE")]
    public class UoGestionnaire
    {
        [Key]
        public int id { get; set; } // Clé primaire de la table de paramétrage
        public string Unite_Gestionnaire { get; set; } = string.Empty;
        public bool Droit_Hygiene { get; set; }
        public bool Droit_Prod { get; set; }
        public string? MAT_RESP { get; set; }
        public string? NOM_PRENOM_RESP { get; set; }
        public string? Utilisateur { get; set; }
        public DateTime? Date_Mvt { get; set; }
    }
}