using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Matiere
    {
        [Key]
        public int IdMatiere { get; set; }

        [Required]
        public string NomMatiere { get; set; } = string.Empty;

        [Required]
        public float Coefficient { get; set; }

        // Many-to-Many avec Professeur
        public ICollection<Professeur> Professeurs { get; set; } = new List<Professeur>();
        // Relation 1-N avec EmploiDuTemps
        public ICollection<EmploiDuTemps> EmploisDuTemps { get; set; } = new List<EmploiDuTemps>();

    }
}
