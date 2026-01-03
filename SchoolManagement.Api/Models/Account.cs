using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Account
    {
        [Key]
        public int IdAccount { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = string.Empty;

        public DateTime DateCreation { get; set; } = DateTime.Now;
        public bool Actif { get; set; } = true;

        // 🔗 Relations optionnelles
        public int? EtudiantId { get; set; }
        public int? ProfesseurId { get; set; }
        public int? ParentId { get; set; }
        public int? AdministrateurId { get; set; }

        public Etudiant? Etudiant { get; set; }
        public Professeur? Professeur { get; set; }
        public Parent? Parent { get; set; }
        public Administrateur? Administrateur { get; set; }

        // 🔗 Relation avec Notifications
        public List<Notification> Notifications { get; set; } = new();
    }
}
