namespace GestoPrime.DTOS
{
    public class LoginRequestDto
    {
        // Correspond au champ 'Login' de votre vue SQL
        public string Username { get; set; } = string.Empty;

        // Correspond au champ 'MPt' de votre vue SQL
        public string Password { get; set; } = string.Empty;
    }
}
