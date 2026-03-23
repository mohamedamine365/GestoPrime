using Microsoft.EntityFrameworkCore;
using GestoPrime.model;
using GestoPrime.DTOS;

namespace GestoPrime.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Déclaration des tables
        public DbSet<User> Users { get; set; }
        public DbSet<UserAuth> UserAuths { get; set; }
        public DbSet<JournalMouvement> Mouvements { get; set; }
        public DbSet<Salarie> Salaries { get; set; }

        // Cette entité est maintenant mappée sur la table de paramétrage pour permettre les updates
        public DbSet<UoGestionnaire> UoGestionnaires { get; set; }

        public DbSet<MoisSalarie> MoisSalarie { get; set; }
        public DbSet<MoisScoreBrut> MoisScoreBrut { get; set; }

        // Déclaration de la vue (pour la consultation uniquement)
        public DbSet<model.DroitsPrimeDto> DroitsPrimes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapping des tables standards
            modelBuilder.Entity<User>().ToTable("T_SIRH_USER");
            modelBuilder.Entity<UserAuth>().ToTable("V_SIRH_Param_Login");
            modelBuilder.Entity<JournalMouvement>().ToTable("T_SIRH_JOURNAL_MOUVEMENT");
            modelBuilder.Entity<Salarie>().ToTable("T_EXP_SALARIE");
            modelBuilder.Entity<MoisSalarie>().ToTable("T_INT_MOIS_SALARIE");
            modelBuilder.Entity<MoisScoreBrut>().ToTable("T_INT_MOIS_SCORE_PPM_BRUT");

            // --- CONFIGURATION CRITIQUE POUR LA MISE À JOUR ---
            // On mappe UoGestionnaire sur la table de paramétrage qui possède les colonnes de droits
            modelBuilder.Entity<UoGestionnaire>(entity =>
            {
                entity.ToTable("T_PARAM_UNITE_GESTIONNAIRE"); //

                // Définition de la clé primaire 'id' vue dans votre SQL
                entity.HasKey(e => e.id); //

                // Mapping explicite des colonnes pour correspondre à la table physique
                entity.Property(e => e.Unite_Gestionnaire).HasColumnName("Unite_Gestionnaire"); //
                entity.Property(e => e.Droit_Hygiene).HasColumnName("Droit_Hygiene"); //
                entity.Property(e => e.Droit_Prod).HasColumnName("Droit_Prod"); //
            });

            // Configuration de la vue SQL (Keyless / Read-only)
            modelBuilder.Entity<model.DroitsPrimeDto>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("V_CONSULTATION_DROITS_PRIMES"); //
            });
        }
    }
}