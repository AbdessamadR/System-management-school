using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Classe
    {
        [Key]
        public int IdClasse { get; set; }

        [Required]
        public string NomClasse { get; set; } = string.Empty;

        [Required]
        public string Niveau { get; set; } = string.Empty;

        [Required]
        public string AnneeScolaire { get; set; } = string.Empty;

        // Relations
        public List<Etudiant> Etudiants { get; set; } = new();

        public List<EmploiDuTemps> EmploisDuTemps { get; set; } = new();
    }
}
