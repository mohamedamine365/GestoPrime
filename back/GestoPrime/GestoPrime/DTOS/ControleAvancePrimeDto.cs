namespace GestoPrime.DTOS
{
    public class ControleAvancePrimeDto
    {
        public string? Periode { get; set; }
        public string? Matricule { get; set; }
        public string? NomPrenom { get; set; }
        public string? Statut { get; set; }
        public decimal? Plafond { get; set; }
        public double? NbJoursTravaille { get; set; }
        public double? NbJoursConge { get; set; }
        public double? NbJoursCongeNonPaye { get; set; }
    }
}
