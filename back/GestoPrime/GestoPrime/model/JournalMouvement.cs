using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class JournalMouvement
{
    [Key]
    public int Id { get; set; }
    public string? Matricule { get; set; }
    [Column("Identite")]
    public string? NomPrenom { get; set; }
    [Column("Site_Centralisateur")]
    public string? Site { get; set; }
    public string? Resultat { get; set; }
    public string? Etablissement { get; set; }
    public DateTime? Date_Mvt { get; set; }
    public string? Nom_Base { get; set; }
    public string? Session { get; set; }
}