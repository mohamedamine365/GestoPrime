namespace GestoPrime.Dtos
{
    public class ControleTauxPrimesDto
    {
        public string Periode { get; set; } = string.Empty;

        public string? UniteGestionnaire { get; set; }

        public double NombreJoursOuvrables { get; set; }

        public double CoefHygiene { get; set; }

        public double CoefProductivite { get; set; }

        public string? Statut { get; set; }
    }
}