using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestoPrime.Models
{
    [Table("V_CONTROLE_DROITS_PRIMES")]
    public class ControlDroitsPrimes
    {
        [Key] // Obligatoire pour EF Core : on définit l'unité comme clé unique
        [Column("Unite_Gestionnaire")]
        public string Unite_Gestionnaire { get; set; }

        [Column("MAT_RESP")]
        public string MAT_RESP { get; set; }

        [Column("NOM_PRENOM_RESP")]
        public string NOM_PRENOM_RESP { get; set; }

        [Column("Droit_Hygiene")]
        public bool Droit_Hygiene { get; set; }

        [Column("Droit_Prod")]
        public bool Droit_Prod { get; set; }
    }
}