using System;
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

        /// <summary>
        /// "Admin", "Professeur", "Etudiant", "Parent"
        /// </summary>
        [Required]
        public string Role { get; set; } = string.Empty;

        public DateTime DateCreation { get; set; } = DateTime.Now;

        public bool Actif { get; set; } = true;

        // Relation : un compte peut appartenir à un administrateur
        public Administrateur? Administrateur { get; set; }
    }
}
