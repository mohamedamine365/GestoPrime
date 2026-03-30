namespace GestoPrime.DTOS
{
    public class ControlePlafondPrimeDto
    {
        // Utilisation de '?' pour permettre les valeurs NULL provenant de la vue SQL
        public string? Periode { get; set; }
        public string? Statut { get; set; }
        public string? UniteGestionnaire { get; set; }
        public string? Matricule { get; set; }
        public string? NomPrenom { get; set; }
        public string? MatriculeResp { get; set; }
        public string? NomPrenomResp { get; set; }

        // Déjà nullables, c'est parfait pour les colonnes vides en base
        public DateTime? DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        public string? CodeRubrique { get; set; }
        public decimal? Montant { get; set; }
    }
}