using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

[Table("T_SIRH_USER")]
public class User
{
    [Key]
    public string Matricule { get; set; } = string.Empty;

    [Column("Identite")]
    [JsonPropertyName("nom_Prenom")]
    public string? NomPrenom { get; set; } // Le '?' autorise le NULL de SQL

    public string? Etablissement { get; set; }

    public string? Privilege { get; set; }

    
    public string? Utilisateur { get; set; }

    public DateTime? Date_Mvt { get; set; }
}