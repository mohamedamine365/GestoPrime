using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestoPrime.model
{
    [Table("T_EXP_UO_GESTIONNAIRE")]
    public class UoGestionnaire
    {
       
            [Key]
            public string CODE_UO { get; set; } = string.Empty;

            public string? LIBELLE_UO { get; set; }

            [Column("UO_GESTIONNAIRE")]
            public string? NomGestionnaire { get; set; }

            // Liens avec la structure géographique
            public string? COD_SIT_CENT { get; set; }
            public string? LIB_LON_SIT_CENT { get; set; }
            public string? COD_ETAB { get; set; }
            public string? LIB_ETAB { get; set; }

            [Column("CODE_MINIPOLE")]
            public string? CodeMinipole { get; set; }
        
    }
}
