using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("T_INT_PARAM_MOIS_UNITE_GESTIONNAIRE")]
public class TauxPrime
{
    [Key]
    public int id { get; set; }
    public string? Periode { get; set; }
    public string? Statut { get; set; }
    public string? Unite_Gestionnaire { get; set; }
    public double NBR_JOUR_OUV { get; set; }
    public double Cof_HYGIENE { get; set; }
    public double Cof_PROD { get; set; }
    public string? Utilisateur { get; set; } // <--- On va stocker le matricule ici
    public string? Mat_User_Lancement { get; set; }

    // Correction : SQL BIT -> C# bool
    public bool Temoin_Confirmation { get; set; }

    public DateTime? Date_Mvt { get; set; }
}