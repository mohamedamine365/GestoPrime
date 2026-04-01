using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestoPrime.model
{
    [Table("T_PARAM_UNITE_GESTIONNAIRE")]
    public class UoGestionnaire
    {
        [Key]
        [Column("id")]
        public int Id { get; set; } // Convention C# PascalCase

        [Required]
        [Column("Unite_Gestionnaire")]
        public string UniteGestionnaire { get; set; } = string.Empty;

        public bool Droit_Hygiene { get; set; }
        public bool Droit_Prod { get; set; }

        [StringLength(50)]
        public string? MAT_RESP { get; set; }

        [StringLength(255)]
        public string? NOM_PRENOM_RESP { get; set; }

        public string? Utilisateur { get; set; }
        public DateTime? Date_Mvt { get; set; }
    }
}