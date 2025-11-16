using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Absence
    {
        [Key]
        public int IdAbsence { get; set; }

        [Required]
        public DateTime DateAbsence { get; set; } = DateTime.Now;

        public bool Justifiee { get; set; }

        // Relations
        [Required]
        public int EtudiantId { get; set; }

        // Navigation property
        public Etudiant? Etudiant { get; set; }
    }
}
