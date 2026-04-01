namespace GestoPrime.DTOS
{
    public class IndemniteDeplacementDto
    {
        public string Periode { get; set; } = "";
        public string Matricule { get; set; } = "";
        public string NomPrenom { get; set; } = "";
        public string StatutSalarie { get; set; } = "";
        public string CodeRubrique { get; set; } = "";
        public decimal Plafond { get; set; }
        public double NbrJourTrav { get; set; }
        public double NbrJourCongePaye { get; set; }
        public double NbrCongeNonPaye { get; set; }
        public double NbrCongePrime { get; set; }
    }
}
