namespace GestoPrime.DTOs
{
    public class SalarieConsultationDto
    {
        public string Periode { get; set; } = string.Empty;
        public string Statut { get; set; } = string.Empty;
        public string UniteGestionnaire { get; set; } = string.Empty;
        public string Matricule { get; set; } = string.Empty;
        public string NomPrenom { get; set; } = string.Empty;
        public string Etablissement { get; set; } = string.Empty;
        public string CodeUnite { get; set; } = string.Empty;
        public string LibelleUnite { get; set; } = string.Empty;

        // Propriétés pour le Responsable
        public string ResponsableInfo { get; set; } = string.Empty;
        public string MatResponsable { get; set; } = string.Empty;
    }
}