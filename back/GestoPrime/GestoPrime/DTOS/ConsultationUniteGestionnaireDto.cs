namespace GestoPrime.DTOS
{
    public class ConsultationUniteGestionnaireDto
    {
        public string UniteGestionnaire { get; set; } = string.Empty;
        public string CodeUnite { get; set; } = string.Empty;
        public string NomPrenom { get; set; } = string.Empty; // Mappe vers LIBELLE_UNITE
    }
}
