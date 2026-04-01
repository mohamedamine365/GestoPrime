namespace GestoPrime.DTOS
{
    public class DroitsPrimeDto
    {
        public int Id { get; set; } // Recommandé pour identifier l'enregistrement
        public string Unite_Gestionnaire { get; set; } = string.Empty;
        public bool Droit_Hygiene { get; set; }
        public bool Droit_Prod { get; set; }
        public string? Utilisateur { get; set; }
    }
}