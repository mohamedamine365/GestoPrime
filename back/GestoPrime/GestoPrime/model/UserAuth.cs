using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestoPrime.model
{
    [Table("V_SIRH_Param_Login")]
    public class UserAuth
    {
        [Key]
        public string Matricule { get; set; } = string.Empty;

        public string Nom_Prenom { get; set; }

        public string? Login { get; set; }

        public string? MPt { get; set; }

        public string? Privilege { get; set; }

        // Utilise ce champ pour identifier le site/établissement dans ton journal
        public string? etablissement { get; set; }

        [Column("COMPTE_LOTUS")]
        public string? COMPTE_LOTS { get; set; }

        public string? SystemUser { get; set; }

        public string? Filiales { get; set; }
    }
}