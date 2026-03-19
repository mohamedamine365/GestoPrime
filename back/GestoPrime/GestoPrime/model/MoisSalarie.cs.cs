using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestoPrime.model
{
    [Table("T_INT_MOIS_SALARIE")]
    public class MoisSalarie
    {
      
        [Key]
        public string? MATRICULE { get; set; } // Identifiant unique

        public DateTime DEBUT { get; set; } // NOT NULL en base
        public DateTime Date_FIN { get; set; } // NOT NULL en base

        public string? NOM_PREMON { get; set; }
        public string? COD_GROUPE { get; set; }
        public string? LIBELLE_GROUPE { get; set; }
        public string? COD_POLE { get; set; }
        public string? LIBELLE_POLE { get; set; }
        public string? COD_SITE { get; set; }
        public string? LIBELLE_SITE { get; set; }
        public string? COD_ETAB { get; set; }
        public string? LIBELLE_ETAB { get; set; }
        public string? MATRICULE_RESP { get; set; }
        public string? NOM_PRENOM_RESP { get; set; }
        public string? COMPTE_LOTUS_RESP { get; set; }
        public string? COD_UNITE { get; set; }
        public string? LIBELLE_UNITE { get; set; }
        public string? UNITE_GEST { get; set; }
        public string? TYPE_CONTRAT { get; set; }
        public string? STATUT { get; set; }
        public string? Matricule_User { get; set; }
        public DateTime? Date_Mvt { get; set; }
    }
}