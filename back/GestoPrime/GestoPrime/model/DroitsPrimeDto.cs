namespace GestoPrime.model
{
    public class DroitsPrimeDto
    {
        public string MAT_RESP { get; set; } = string.Empty;
        public string NOM_PRENOM_RESP { get; set; } = string.Empty;
        public string Unite_Gestionnaire { get; set; } = string.Empty;
        public bool Droit_Hygiene { get; set; }
        public bool Droit_Prod { get; set; }
    }
}
