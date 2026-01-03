using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SchoolManagementSystem.Models
{
    public class Absence
    {
        [Key]
        public int IdAbsence { get; set; }

        [Required]
        public DateTime DateAbsence { get; set; } = DateTime.Now;

        public bool Justifiee { get; set; }

        [Required]
        public int EtudiantId { get; set; }

        // Navigation property
        [JsonIgnore]
        public Etudiant? Etudiant { get; set; }
        
        public int? MatiereId { get; set; }
        public Matiere? Matiere { get; set; }

    }
}
