using System.ComponentModel.DataAnnotations.Schema;

[Table("V_CONTROLE_PLAFOND_PRIME")]
public class ControlePlafondPrime
{
    [Column("PERIODE")]
    public string? Periode { get; set; }

    [Column("STATUT")]
    public string? Statut { get; set; }

    [Column("UNITE_GESTIONNAIRE")] // <--- Très important
    public string? UniteGestionnaire { get; set; }

    [Column("MATRICULE")]
    public string? Matricule { get; set; }

    [Column("NOM_PRENOM")] // <--- Très important
    public string? NomPrenom { get; set; }

    [Column("MATRICULE_RESP")]
    public string? MatriculeResp { get; set; }

    [Column("NOM_PRENOM_RESP")]
    public string? NomPrenomResp { get; set; }

    [Column("DATE_DEBUT")]
    public DateTime? DateDebut { get; set; }

    [Column("DATE_FIN")]
    public DateTime? DateFin { get; set; }

    [Column("COD_RUB")]
    public string? CodRub { get; set; }

    [Column("MONTANT")]
    public decimal? Montant { get; set; }
}