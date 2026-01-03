using System.Collections.Generic;

namespace SchoolManagementSystem.DTOs
{
    public class ParentDTO
    {
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public string Adresse { get; set; } = string.Empty;
        public string MotDePasse { get; set; } = string.Empty;
        public List<int> EtudiantsIds { get; set; } = new();
    }
}
