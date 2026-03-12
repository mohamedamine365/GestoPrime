using GestoPrime.Data;
using GestoPrime.DTOS;
using GestoPrime.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GestoPrime.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public AuthController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var user = await _context.UserAuths
                .FirstOrDefaultAsync(u => u.Login == request.Username && u.MPt == request.Password);

            var journal = new JournalMouvement
            {
                Date_Mvt = DateTime.Now,
                Matricule = user?.Matricule ?? request.Username,
                NomPrenom = user?.Nom_Prenom ?? "Identité Inconnue",
                Site = user?.etablissement ?? "SITE_WEB",
                Resultat = user != null ? "Success" : "Failed",
                Session = "Connexion_Web",
                Nom_Base = "GestoPrime_DB",
                Etablissement = user?.etablissement
                // Ne PAS ajouter UoGestionnaire ici si la colonne n'existe pas en SQL
            };

            _context.Mouvements.Add(journal);
            await _context.SaveChangesAsync(); // L'erreur se produisait ici

            if (user == null) return Unauthorized(new { message = "Identifiants invalides" });

            return Ok(new AuthResponseDto
            {
                Token = GenerateToken(user),
                Matricule = user.Matricule,
                NomPrenom = user.Nom_Prenom,
                Role = user.Privilege
            });
        }

        private string GenerateToken(UserAuth user)
        {
            var claims = new[] {
                new Claim(ClaimTypes.Name, user.Login ?? ""),
                new Claim(ClaimTypes.Role, user.Privilege ?? "User"),
                new Claim("Matricule", user.Matricule)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? "Cle_Securite_Par_Defaut_32_Chars"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}