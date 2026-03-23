namespace GestoPrime.DTOS
{
    public class DroitsPrimeDto
    {
        public string Unite_Gestionnaire { get; set; } = string.Empty;
        public string? MAT_RESP { get; set; }
        public string? NOM_PRENOM_RESP { get; set; }
        public bool Droit_Hygiene { get; set; }
        public bool Droit_Prod { get; set; }
    }
}
