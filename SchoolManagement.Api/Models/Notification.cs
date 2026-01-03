using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

        // Option 1 : notification pour un étudiant précis
        public int? EtudiantId { get; set; }
        [JsonIgnore]
        public Etudiant? Etudiant { get; set; }

        // Option 2 : notification pour une classe entière
        public int? ClasseId { get; set; }
        [JsonIgnore]
        public Classe? Classe { get; set; }

        // Optionnel : lié à un compte si tu veux garder Account
        public int? AccountId { get; set; }
        [JsonIgnore]
        public Account? Destinataire { get; set; }
    }
}
