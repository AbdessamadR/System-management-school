using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Notification
    {
        [Key]
        public int IdNotification { get; set; }

        [Required]
        public string Contenu { get; set; } = string.Empty;

        [Required]
        public DateTime DateEnvoi { get; set; } = DateTime.Now;

        [Required]
        public string Type { get; set; } = string.Empty; // Exemple : "Absence", "Message", "Alerte"

        // Relation
        [Required]
        public int AccountId { get; set; }

        public Account? Destinataire { get; set; }
    }
}
