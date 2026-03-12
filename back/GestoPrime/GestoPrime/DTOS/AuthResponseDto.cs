namespace GestoPrime.DTOS
{
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public string Matricule { get; set; }

        // On utilise un nom clair pour le frontend
        public string NomPrenom { get; set; }

        // Contient la valeur de 'Privilege' (Admin, Evaluation, etc.)
        public string Role { get; set; }
    }
}
