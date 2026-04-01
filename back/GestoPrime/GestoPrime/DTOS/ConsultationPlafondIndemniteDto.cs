namespace GestoPrime.DTOS
{
    public class ConsultationPlafondIndemniteDto
    {
        public string Matricule { get; set; } = string.Empty;
        public string NomPrenom { get; set; } = string.Empty;
        public string DateDebut { get; set; } = string.Empty;
        public string DateFin { get; set; } = string.Empty;
        public string CodeRubrique { get; set; } = string.Empty;
        public decimal Montant { get; set; }
    }
}
