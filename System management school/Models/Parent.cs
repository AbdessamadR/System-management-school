using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Parent
    {
        [Key]
        public int IdParent { get; set; }

        [Required]
        public string Nom { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Telephone { get; set; } = string.Empty;

        [Required]
        public string Adresse { get; set; } = string.Empty;

        // Relations
        public List<Etudiant> Enfants { get; set; } = new();
    }
}
