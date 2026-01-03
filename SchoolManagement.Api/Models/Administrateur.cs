using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Administrateur
    {
        [Key]
        public int IdAdmin { get; set; }

        [Required]
        public string Nom { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = "Admin";

        // Foreign Key
        [Required]
        public int AccountId { get; set; }

        // Navigation property
        public Account? Account { get; set; }
    }
}
