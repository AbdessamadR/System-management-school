using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Professeur
    {
        [Key]
        public int IdProf { get; set; }

        [Required]
        public string Nom { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Specialite { get; set; } = string.Empty;

        // Relations
        public List<Matiere> Matieres { get; set; } = new();
        public List<EmploiDuTemps> EmploisDuTemps { get; set; } = new();
        public List<Account> Accounts { get; set; } = new();
    }
}
