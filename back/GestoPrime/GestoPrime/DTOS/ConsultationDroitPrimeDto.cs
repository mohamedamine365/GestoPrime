namespace GestoPrime.DTOS
{
    public class ConsultationDroitPrimeDto
    {
        public string UniteGestionnaire { get; set; } = string.Empty;
        public string MatriculeResp { get; set; } = string.Empty;
        public string NomPrenomResp { get; set; } = string.Empty;
        public bool DroitHygiene { get; set; }
        public bool DroitProductivite { get; set; }
    }
}
