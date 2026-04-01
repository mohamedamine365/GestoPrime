using System.ComponentModel.DataAnnotations.Schema;

namespace GestoPrime.Models
{
    // On indique à EF Core que ce modèle correspond à la vue SQL spécifique
    [Table("V_CONTROLE_TAUX_PRIMES")]
    public class ControleTauxPrimes
    {
        public string Periode { get; set; } = string.Empty;
        public string? Unite_Gestionnaire { get; set; }

        // NBR_JOUR_OUV doit être en double (vu précédemment)
        public double NBR_JOUR_OUV { get; set; }

        // CORRECTION ICI : Remplacer decimal par double
        public double Cof_HYGIENE { get; set; }
        public double COF_PROD { get; set; }

        public string? Statut { get; set; }
    }
}