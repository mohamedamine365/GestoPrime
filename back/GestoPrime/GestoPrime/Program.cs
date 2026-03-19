using GestoPrime.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


namespace GestoPrime
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
           

            // --- 1. ENREGISTREMENT DES SERVICES ---

            builder.Services.AddControllers();

            // Connexion SQL Server (Chaîne de connexion dans appsettings.json)
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Configuration CORS : Autorise les appels depuis un navigateur
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy.AllowAnyOrigin()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
            });

            // Configuration Sécurité JWT
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    var jwtKey = builder.Configuration["Jwt:Key"];
                    if (string.IsNullOrEmpty(jwtKey))
                    {
                        throw new Exception("La clé JWT est manquante dans la configuration.");
                    }

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero // Pour une expiration précise
                    };
                });

            builder.Services.AddAuthorization();

            // Configuration Swagger Intelligente
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GestoPrime API", Version = "v1" });

                // Gère le préfixe "Bearer " automatiquement
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Collez uniquement votre jeton JWT ici (sans 'Bearer ' et sans accolades)",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new string[] {}
                    }
                });
            });

            // --- 2. CONSTRUCTION ---
            var app = builder.Build();

            // --- 3. PIPELINE DE REQUÊTES (Middleware) ---

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GestoPrime API v1");
                });
            }

            app.UseHttpsRedirection();

            // Le CORS doit être APRES UseHttpsRedirection et AVANT UseAuthentication
            app.UseCors("AllowAll");

            app.UseAuthentication(); // Vérifie le Token
            app.UseAuthorization();  // Vérifie les Rôles

            app.MapControllers();

            app.Run();
        }
    }
}