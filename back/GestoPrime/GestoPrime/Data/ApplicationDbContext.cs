using GestoPrime.DTOS;
using GestoPrime.model;
using GestoPrime.model.GestoPrime.Models;
using GestoPrime.Models;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<ControleIndemniteDeplacement> IndemnitesDeplacement { get; set; }
        public DbSet<ControleAvancePrime> V_CONTROLE_AVANCE_PRIME { get; set; }
        public DbSet<ControleUniteGestionnaire> V_CONTROLE_UNITE_GESTIONNAIRE { get; set; }
        public DbSet<ControlePointage> V_CONTROLE_POINTAGE { get; set; }
        public DbSet<ControlDroitsPrimes> ControlDroitsPrimes { get; set; }
        public DbSet<ControleTauxPrimes> ControleTauxPrimes { get; set; }
        public DbSet<ConsultationPlafondPrimeRendement> ConsultationPlafondPrimeRendements { get; set; }


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



            modelBuilder.Entity<UoGestionnaire>(entity =>
            {
                entity.ToTable("T_PARAM_UNITE_GESTIONNAIRE");
                entity.HasKey(e => e.Id);

                // Assurer la correspondance exacte avec les noms SQL si nécessaire
                entity.Property(e => e.UniteGestionnaire).HasColumnName("Unite_Gestionnaire");
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
                entity.HasNoKey();
                entity.ToView("V_CONSULTATION_POINTAGE");
            });

            modelBuilder.Entity<ConsultationSalarie>().HasNoKey().ToView("V_CONSULTATION_SALARIE");

            modelBuilder.Entity<ControlePlafondPrime>().HasNoKey().ToView("V_CONTROLE_PLAFOND_PRIME");

            modelBuilder.Entity<ControleIndemniteDeplacement>(eb =>
            {
                eb.HasNoKey(); // Ou HasKey(x => x.MATRICULE)
                eb.ToView("V_CONTROLE_CALCUL_PRIME_INDEMNITE_DEPLACEMENT");
            });

            modelBuilder.Entity<ControleAvancePrime>(entity =>
            {
                // Indique à EF que cette entité n'a pas de clé primaire (Vue SQL)
                entity.HasNoKey();

                // Mappe l'entité au nom exact de la vue dans la base de données
                entity.ToView("V_CONTROLE_CALCUL_PRIME_AVANCE_PRIME_RESULTAT");

                // Optionnel : Si vos colonnes SQL ont des types spécifiques (ex: decimal 18,3)
                entity.Property(e => e.PLAFOND).HasColumnType("decimal(18, 3)");
            });

            modelBuilder.Entity<ControleUniteGestionnaire>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("V_CONTROLE_UNITE_GESTIONNAIRE");
            });

            modelBuilder.Entity<ControlePointage>(entity =>
            {
                entity.HasNoKey(); // C'est une vue sans clé primaire
                entity.ToView("V_CONTROLE_POINTAGE");
            });

            modelBuilder.Entity<ControlDroitsPrimes>(entity =>
            {
                // On confirme à EF que la clé est Unite_Gestionnaire
                entity.HasKey(e => e.Unite_Gestionnaire);

                // On mappe sur la vue SQL
                entity.ToView("V_CONTROLE_DROITS_PRIMES");
            });

            modelBuilder.Entity<ControleTauxPrimes>(entity =>
            {
                entity.HasNoKey(); // Indispensable pour une vue SQL sans ID unique
                entity.ToView("V_CONTROLE_TAUX_PRIMES");
            });

            modelBuilder.Entity<ConsultationPlafondPrimeRendement>().HasNoKey();
        }
    }
}