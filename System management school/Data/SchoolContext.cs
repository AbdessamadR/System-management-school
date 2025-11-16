using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }

        // Tables
        public DbSet<Etudiant> Etudiants { get; set; }
        public DbSet<Professeur> Professeurs { get; set; }
        public DbSet<Classe> Classes { get; set; }
        public DbSet<Matiere> Matieres { get; set; }
        public DbSet<Absence> Absences { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Administrateur> Administrateurs { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<EmploiDuTemps> EmploisDuTemps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Classe ↔ Etudiants (1-N)
            modelBuilder.Entity<Classe>()
                .HasMany(c => c.Etudiants)
                .WithOne(e => e.Classe)
                .HasForeignKey(e => e.ClasseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Classe ↔ EmploisDuTemps (1-N)
            modelBuilder.Entity<Classe>()
                .HasMany(c => c.EmploisDuTemps)
                .WithOne(e => e.Classe)
                .HasForeignKey(e => e.ClasseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Etudiant ↔ Absences (1-N)
            modelBuilder.Entity<Etudiant>()
                .HasMany(e => e.Absences)
                .WithOne(a => a.Etudiant)
                .HasForeignKey(a => a.EtudiantId)
                .OnDelete(DeleteBehavior.Cascade);

            // Parent ↔ Etudiants (1-N)
            modelBuilder.Entity<Parent>()
                .HasMany(p => p.Enfants)
                .WithOne(e => e.Parent)
                .HasForeignKey(e => e.ParentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Professeur ↔ Matieres (1-N)
            modelBuilder.Entity<Professeur>()
                .HasMany(p => p.Matieres)
                .WithOne(m => m.Professeur)
                .HasForeignKey(m => m.ProfesseurId)
                .OnDelete(DeleteBehavior.Cascade);

            // Professeur ↔ EmploisDuTemps (1-N)
            modelBuilder.Entity<Professeur>()
                .HasMany(p => p.EmploisDuTemps)
                .WithOne(e => e.Professeur)
                .HasForeignKey(e => e.ProfesseurId)
                .OnDelete(DeleteBehavior.Restrict); // <-- Modifié pour éviter les conflits

            // Account ↔ Administrateur (1-1)
            modelBuilder.Entity<Administrateur>()
                .HasOne(a => a.Account)
                .WithOne(ac => ac.Administrateur)
                .HasForeignKey<Administrateur>(a => a.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            // Matiere ↔ EmploisDuTemps (1-N)
            modelBuilder.Entity<Matiere>()
                .HasMany<EmploiDuTemps>()
                .WithOne(e => e.Matiere)
                .HasForeignKey(e => e.MatiereId)
                .OnDelete(DeleteBehavior.Restrict); // <-- Modifié pour éviter les conflits

            // Account ↔ Notifications (1-N)
            modelBuilder.Entity<Account>()
                .HasMany<Notification>()
                .WithOne(n => n.Destinataire)
                .HasForeignKey(n => n.AccountId)
                .OnDelete(DeleteBehavior.Cascade);
        }


    }
}
