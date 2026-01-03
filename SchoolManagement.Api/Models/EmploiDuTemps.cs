using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class EmploiDuTemps
    {
        [Key]
        public int IdEmploi { get; set; }

        [Required]
        [MaxLength(20)]
        public string Jour { get; set; } = string.Empty;

        [Required]
        public TimeSpan HeureDebut { get; set; }

        [Required]
        public TimeSpan HeureFin { get; set; }

        // Salle (texte simple)
        [Required]
        [MaxLength(50)]
        public string Salle { get; set; } = string.Empty;

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
