using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagement.Api.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly SchoolContext _context;

        public ClassesController(SchoolContext context)
        {
            _context = context;
        }

        // GET: api/admin/classes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Classe>>> GetClasses()
        {
            return await _context.Classes
                .Include(c => c.Professeurs)
                .Include(c => c.Etudiants)
                .ToListAsync();
        }

        // GET: api/admin/classes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Classe>> GetClasse(int id)
        {
            var classe = await _context.Classes
                .Include(c => c.Professeurs)
                .Include(c => c.Etudiants)
                .FirstOrDefaultAsync(c => c.IdClasse == id);

            if (classe == null) return NotFound();
            return classe;
        }

        // POST: api/admin/classes
        [HttpPost]
        public async Task<ActionResult<Classe>> CreateClasse(Classe classe)
        {
            _context.Classes.Add(classe);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClasse), new { id = classe.IdClasse }, classe);
        }

        // PUT: api/admin/classes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClasse(int id, Classe classe)
        {
            if (id != classe.IdClasse) return BadRequest("L'ID ne correspond pas.");

            var dbClasse = await _context.Classes.FindAsync(id);
            if (dbClasse == null) return NotFound();

            dbClasse.NomClasse = classe.NomClasse;
            dbClasse.Niveau = classe.Niveau;
            dbClasse.AnneeScolaire = classe.AnneeScolaire;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/admin/classes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClasse(int id)
        {
            var classe = await _context.Classes.FindAsync(id);
            if (classe == null) return NotFound();

            _context.Classes.Remove(classe);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // =============================
        // Gestion Many-to-Many Classe ↔ Professeur
        // =============================

        // Ajouter un prof à une classe
        [HttpPost("{classeId}/addprof/{profId}")]
        public async Task<IActionResult> AddProfesseurToClasse(int classeId, int profId)
        {
            var classe = await _context.Classes
                .Include(c => c.Professeurs)
                .FirstOrDefaultAsync(c => c.IdClasse == classeId);
            var prof = await _context.Professeurs.FindAsync(profId);

            if (classe == null || prof == null) return NotFound();

            if (!classe.Professeurs.Contains(prof))
                classe.Professeurs.Add(prof);

            await _context.SaveChangesAsync();
            return Ok(classe);
        }

        // Retirer un prof d'une classe
        [HttpPost("{classeId}/removeprof/{profId}")]
        public async Task<IActionResult> RemoveProfesseurFromClasse(int classeId, int profId)
        {
            var classe = await _context.Classes
                .Include(c => c.Professeurs)
                .FirstOrDefaultAsync(c => c.IdClasse == classeId);
            var prof = await _context.Professeurs.FindAsync(profId);

            if (classe == null || prof == null) return NotFound();

            if (classe.Professeurs.Contains(prof))
                classe.Professeurs.Remove(prof);

            await _context.SaveChangesAsync();
            return Ok(classe);
        }
    }
}
