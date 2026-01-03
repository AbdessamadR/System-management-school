using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SchoolManagement.Api.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class MatieresController : ControllerBase
    {
        private readonly SchoolContext _context;

        public MatieresController(SchoolContext context)
        {
            _context = context;
        }

        // GET: api/admin/matieres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Matiere>>> GetMatieres()
        {
            return await _context.Matieres
                .Include(m => m.Professeurs)
                .ToListAsync();
        }

        // GET: api/admin/matieres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Matiere>> GetMatiere(int id)
        {
            var matiere = await _context.Matieres
                .Include(m => m.Professeurs)
                .FirstOrDefaultAsync(m => m.IdMatiere == id);

            if (matiere == null) return NotFound();
            return matiere;
        }

        // POST: api/admin/matieres
        [HttpPost]
        public async Task<ActionResult<Matiere>> PostMatiere(Matiere matiere)
        {
            _context.Matieres.Add(matiere);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMatiere), new { id = matiere.IdMatiere }, matiere);
        }

        // PUT: api/admin/matieres/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatiere(int id, Matiere matiere)
        {
            if (id != matiere.IdMatiere)
                return BadRequest("Id de la matière incorrect");

            var matiereExistante = await _context.Matieres
                .Include(m => m.Professeurs)
                .FirstOrDefaultAsync(m => m.IdMatiere == id);

            if (matiereExistante == null) return NotFound();

            // Mise à jour des champs corrects
            matiereExistante.NomMatiere = matiere.NomMatiere;
            matiereExistante.Coefficient = matiere.Coefficient;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/admin/matieres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatiere(int id)
        {
            var matiere = await _context.Matieres.FindAsync(id);
            if (matiere == null) return NotFound();

            _context.Matieres.Remove(matiere);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
