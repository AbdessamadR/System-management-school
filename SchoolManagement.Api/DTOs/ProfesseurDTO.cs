namespace SchoolManagement.Api.DTOs
{
    public class ProfesseurDTO
    {
        public int IdProf { get; set; }
        public string Nom { get; set; }
        public string Email { get; set; }
        public string SpecialiteId { get; set; }
        public string SpecialiteNom { get; set; } // Le champ que React va afficher
        public List<ClasseDTO> Classes { get; set; } = new();
    }

    public class ClasseDTO
    {
        public int IdClasse { get; set; }
        public string NomClasse { get; set; }
    }
}