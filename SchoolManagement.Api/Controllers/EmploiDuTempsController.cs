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
    public class EmploiDuTempsController : ControllerBase
    {
        private readonly SchoolContext _context;

        public EmploiDuTempsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: api/admin/emploiDuTemps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmploiDuTemps>>> GetAll()
        {
            return await _context.EmploisDuTemps
                .Include(e => e.Classe)
                .Include(e => e.Matiere)
                .Include(e => e.Professeur)
                .ToListAsync();
        }

        // GET: api/admin/emploiDuTemps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmploiDuTemps>> GetById(int id)
        {
            var emploi = await _context.EmploisDuTemps
                .Include(e => e.Classe)
                .Include(e => e.Matiere)
                .Include(e => e.Professeur)
                .FirstOrDefaultAsync(e => e.IdEmploi == id);

            if (emploi == null) return NotFound();
            return emploi;
        }

        // POST: api/admin/emploiDuTemps
        [HttpPost]
        public async Task<ActionResult<EmploiDuTemps>> Create(EmploiDuTemps emploi)
        {
            // Validation manuelle si besoin
            if (emploi.HeureFin <= emploi.HeureDebut)
                return BadRequest("L'heure de fin doit être supérieure à l'heure de début.");

            _context.EmploisDuTemps.Add(emploi);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = emploi.IdEmploi }, emploi);
        }

        // PUT: api/admin/emploiDuTemps/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EmploiDuTemps emploi)
        {
            if (id != emploi.IdEmploi)
                return BadRequest("Id non valide.");

            var existing = await _context.EmploisDuTemps.FindAsync(id);
            if (existing == null) return NotFound();

            // Mise à jour des champs
            existing.Jour = emploi.Jour;
            existing.HeureDebut = emploi.HeureDebut;
            existing.HeureFin = emploi.HeureFin;
            existing.Salle = emploi.Salle;
            existing.ClasseId = emploi.ClasseId;
            existing.MatiereId = emploi.MatiereId;
            existing.ProfesseurId = emploi.ProfesseurId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/admin/emploiDuTemps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var emploi = await _context.EmploisDuTemps.FindAsync(id);
            if (emploi == null) return NotFound();

            _context.EmploisDuTemps.Remove(emploi);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
