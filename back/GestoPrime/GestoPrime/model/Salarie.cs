using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestoPrime.model
{
    [Table("T_EXP_SALARIE")]
    public class Salarie
    {
            [Key]
            [Column("MATCLE")] 
            public string Matricule { get; set; } = string.Empty;

            [Column("NMPRES")] 
            public string? NomPrenom { get; set; }

            [Column("LIB_VILLE")]
            public string? Ville { get; set; }

            [Column("LIB_ETAB_PAIE")] 
            public string? Etablissement { get; set; }

            [Column("LIBELLE_POSTE")]
            public string? Poste { get; set; }

            [Column("COMPTE_LOTUS")] 
            public string? CompteLotus { get; set; }

            [Column("COMPTE_WINDOWS")]
            public string? CompteWindows { get; set; }

            [Column("LIBELLE_SECTEUR")] 
            public string? Secteur { get; set; }

            [Column("LIBELLE_GROUPE")]
             public string? Groupe { get; set; }

           [Column("Date_Mvt")]
            public DateTime? DateMouvement { get; set; }
        }
    }

