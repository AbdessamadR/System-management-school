using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using SchoolManagement.Api.DTOs;

namespace SchoolManagement.Api.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class ProfesseursController : ControllerBase
    {
        private readonly SchoolContext _context;

        public ProfesseursController(SchoolContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfesseurDTO>>> GetProfesseurs()
        {
            // Récupération des données brutes
            var profs = await _context.Professeurs.Include(p => p.Classes).ToListAsync();
            var matieres = await _context.Matieres.ToListAsync();

            // Mapping vers DTO avec correction du matching
            var result = profs.Select(p => {
                // On nettoie l'ID "5" au cas où il y aurait des espaces en base
                var sId = p.Specialite?.Trim();
                
                // On cherche la matière PHP dont l'ID est 5
                var matiereObj = matieres.FirstOrDefault(m => m.IdMatiere.ToString() == sId);

                return new ProfesseurDTO {
                    IdProf = p.IdProf,
                    Nom = p.Nom,
                    Email = p.Email,
                    SpecialiteId = sId,
                    // Si on trouve la matière, on prend son nom, sinon on affiche l'ID par défaut
                    SpecialiteNom = matiereObj != null ? matiereObj.NomMatiere : (string.IsNullOrEmpty(sId) ? "Non défini" : $"Matière #{sId}"),
                    Classes = p.Classes.Select(c => new ClasseDTO { 
                        IdClasse = c.IdClasse, 
                        NomClasse = c.NomClasse 
                    }).ToList()
                };
            }).ToList();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> PostProfesseur(Professeur prof)
        {
            if (prof.Classes != null)
            {
                var ids = prof.Classes.Select(c => c.IdClasse).ToList();
                prof.Classes = await _context.Classes.Where(c => ids.Contains(c.IdClasse)).ToListAsync();
            }
            _context.Professeurs.Add(prof);
            await _context.SaveChangesAsync();
            return Ok(prof);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfesseur(int id)
        {
            var p = await _context.Professeurs.FindAsync(id);
            if (p == null) return NotFound();
            _context.Professeurs.Remove(p);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}