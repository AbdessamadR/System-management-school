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

        public string? Email { get; set; }
        public DateTime DateNaissance { get; set; }

        public int? ClasseId { get; set; }
        public Classe? Classe { get; set; }

        public int? ParentId { get; set; }
        public Parent? Parent { get; set; }

        // Navigation vers le compte
        public Account? Account { get; set; }

        // 🔹 Navigation vers les absences
        public ICollection<Absence>? Absences { get; set; } = new List<Absence>();
    }
}
