using System.ComponentModel.DataAnnotations.Schema;

namespace GestoPrime.model
{
    [Table("V_CONSULTATION_DROITS_PRIMES")]
    public class ConsultationDroitPrime
    {
        [Column("Unite_Gestionnaire")]
        public string? UniteGestionnaire { get; set; }

        [Column("MAT_RESP")]
        public string? MatResp { get; set; }

        [Column("NOM_PRENOM_RESP")]
        public string? NomPrenomResp { get; set; }

        [Column("Droit_Hygiene")]
        public int DroitHygiene { get; set; } // 1 pour true, 0 pour false

        [Column("Droit_Prod")]
        public int DroitProd { get; set; } // 1 pour true, 0 pour false
    }
}
