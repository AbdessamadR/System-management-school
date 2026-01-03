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

            // =============================
            // Relations Many-to-Many
            // =============================
            modelBuilder.Entity<Professeur>()
                .HasMany(p => p.Classes)
                .WithMany(c => c.Professeurs)
                .UsingEntity(j => j.ToTable("ProfesseurClasse"));

            modelBuilder.Entity<Professeur>()
                .HasMany(p => p.Matieres)
                .WithMany(m => m.Professeurs)
                .UsingEntity(j => j.ToTable("ProfesseurMatiere"));

            // =============================
            // Relations 1-N
            // =============================
            modelBuilder.Entity<Classe>()
                .HasMany(c => c.Etudiants)
                .WithOne(e => e.Classe)
                .HasForeignKey(e => e.ClasseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Classe>()
                .HasMany(c => c.EmploisDuTemps)
                .WithOne(e => e.Classe)
                .HasForeignKey(e => e.ClasseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Etudiant>()
                .HasMany(e => e.Absences)
                .WithOne(a => a.Etudiant)
                .HasForeignKey(a => a.EtudiantId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Parent>()
                .HasMany(p => p.Enfants)
                .WithOne(e => e.Parent)
                .HasForeignKey(e => e.ParentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Professeur>()
                .HasMany(p => p.EmploisDuTemps)
                .WithOne(e => e.Professeur)
                .HasForeignKey(e => e.ProfesseurId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Administrateur>()
                .HasOne(a => a.Account)
                .WithOne(ac => ac.Administrateur)
                .HasForeignKey<Administrateur>(a => a.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Matiere>()
                .HasMany(m => m.EmploisDuTemps)
                .WithOne(e => e.Matiere)
                .HasForeignKey(e => e.MatiereId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.Notifications)
                .WithOne(n => n.Destinataire)
                .HasForeignKey(n => n.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            // =============================
            // Seed Admin par défaut
            // =============================
            var adminAccountId = 1;
            var adminId = 1;

            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    IdAccount = adminAccountId,
                    Username = "admin@school.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                    Role = "Admin",
                    Actif = true
                }
            );

            modelBuilder.Entity<Administrateur>().HasData(
                new Administrateur
                {
                    IdAdmin = adminId,
                    Nom = "Super",
                    Email = "admin@school.com",
                    AccountId = adminAccountId,
                    Role = "Admin"
                }
            );
        }
    }
}
