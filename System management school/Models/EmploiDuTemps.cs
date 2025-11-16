using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class EmploiDuTemps
    {
        [Key]
        public int IdEmploi { get; set; }

        [Required]
        public string Jour { get; set; } = string.Empty;

        [Required]
        public TimeSpan HeureDebut { get; set; }

        [Required]
        public TimeSpan HeureFin { get; set; }

        // Relations
        [Required]
        public int ClasseId { get; set; }
        public Classe? Classe { get; set; }

        [Required]
        public int MatiereId { get; set; }
        public Matiere? Matiere { get; set; }

        [Required]
        public int ProfesseurId { get; set; }
        public Professeur? Professeur { get; set; }
    }
}
