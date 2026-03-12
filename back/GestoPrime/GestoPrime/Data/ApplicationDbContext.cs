using Microsoft.EntityFrameworkCore;
using GestoPrime.model;

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
        public DbSet<UoGestionnaire> UoGestionnaires { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // On s'assure que EF Core ne cherche pas des noms de table au pluriel
            modelBuilder.Entity<User>().ToTable("T_SIRH_USER");
            modelBuilder.Entity<UserAuth>().ToTable("V_SIRH_Param_Login");
            modelBuilder.Entity<JournalMouvement>().ToTable("T_SIRH_JOURNAL_MOUVEMENT");
            modelBuilder.Entity<Salarie>().ToTable("T_EXP_SALARIE");
            modelBuilder.Entity<UoGestionnaire>().ToTable("T_EXP_UO_GESTIONNAIRE\"");



        }
    }
}