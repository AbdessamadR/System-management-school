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

        // Relation
        [Required]
        public int ProfesseurId { get; set; }

        public Professeur? Professeur { get; set; }
    }
}
