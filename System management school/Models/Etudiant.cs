using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Etudiant
    {
        [Key]
        public int IdEtudiant { get; set; }

        [Required]
        public string Nom { get; set; } = string.Empty;

        [Required]
        public string Prenom { get; set; } = string.Empty;

        [Required]
        public DateTime DateNaissance { get; set; }

        [Required]
        public string Email { get; set; } = string.Empty;

        // Relations
        [Required]
        public int ClasseId { get; set; }
        public Classe? Classe { get; set; }

        [Required]
        public int ParentId { get; set; }
        public Parent? Parent { get; set; }

        public List<Absence> Absences { get; set; } = new();
    }
}
