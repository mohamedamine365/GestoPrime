using Microsoft.EntityFrameworkCore;
using GestoPrime.model;
using GestoPrime.DTOS;

namespace GestoPrime.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Déclaration des tables physiques
        public DbSet<User> Users { get; set; }
        public DbSet<UserAuth> UserAuths { get; set; }
        public DbSet<JournalMouvement> Mouvements { get; set; }
        public DbSet<Salarie> Salaries { get; set; }

        // Table de paramétrage par défaut (Droits par UO)
        public DbSet<UoGestionnaire> UoGestionnaires { get; set; }

        public DbSet<MoisSalarie> MoisSalarie { get; set; }
        public DbSet<MoisScoreBrut> MoisScoreBrut { get; set; }

        // Table des taux par période (Tours, Coefs par mois)
        public DbSet<TauxPrime> TauxPrimes { get; set; }
        public DbSet<Periode> Periodes { get; set; }
        public DbSet<Pointage> V_CONSULTATION_POINTAGE { get; set; }
        public DbSet<ConsultationSalarie> V_CONSULTATION_SALARIE { get; set; }
        public DbSet<ControlePlafondPrime> ControlePlafondPrimes { get; set; }

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

            // --- CONFIGURATION DE LA TABLE DE PARAMÉTRAGE UO ---
            modelBuilder.Entity<UoGestionnaire>(entity =>
            {
                entity.ToTable("T_PARAM_UNITE_GESTIONNAIRE");
                entity.HasKey(e => e.id);

                entity.Property(e => e.Unite_Gestionnaire).HasColumnName("Unite_Gestionnaire");
                entity.Property(e => e.Droit_Hygiene).HasColumnName("Droit_Hygiene");
                entity.Property(e => e.Droit_Prod).HasColumnName("Droit_Prod");
            });

            // --- CONFIGURATION DES TAUX PAR PÉRIODE ---
            modelBuilder.Entity<TauxPrime>(entity =>
            {
                entity.ToTable("T_INT_PARAM_MOIS_UNITE_GESTIONNAIRE");
                entity.HasKey(e => e.id);

                // Mapping des colonnes numériques
                entity.Property(e => e.Cof_HYGIENE).HasColumnType("float");
                entity.Property(e => e.Cof_PROD).HasColumnType("float");
                entity.Property(e => e.NBR_JOUR_OUV).HasColumnName("NBR_JOUR_OUV");
            });

            modelBuilder.Entity<Periode>(entity =>
            {
                entity.ToTable("T_INT_PERIODE");
                entity.Property(e => e.Periode_Val).HasColumnName("Periode"); // Mappe Periode_Val vers Periode
            });
           
            modelBuilder.Entity<Pointage>(entity =>
            {
                entity.HasNoKey(); // INDISPENSABLE pour une vue
                entity.ToView("V_CONSULTATION_POINTAGE"); // Vérifie bien le nom exact ici
            });

            modelBuilder.Entity<ConsultationSalarie>().HasNoKey().ToView("V_CONSULTATION_SALARIE");

            modelBuilder.Entity<ControlePlafondPrime>().HasNoKey().ToView("V_CONTROLE_PLAFOND_PRIME");

        }
    }
}