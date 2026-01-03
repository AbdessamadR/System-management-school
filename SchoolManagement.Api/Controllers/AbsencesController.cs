using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;

namespace SchoolManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbsencesController : ControllerBase
    {
        private readonly SchoolContext _context;

        public AbsencesController(SchoolContext context)
        {
            _context = context;
        }

        // GET: api/Absences
       [HttpGet]
public async Task<ActionResult<IEnumerable<Absence>>> GetAbsences()
{
    var absences = await _context.Absences
        .Include(a => a.Etudiant) // 🔹 important
        .ToListAsync();

    return Ok(absences);
}


        // GET: api/Absences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Absence>> GetAbsence(int id)
        {
            var absence = await _context.Absences
                .Include(a => a.Etudiant)
                .FirstOrDefaultAsync(a => a.IdAbsence == id);

            if (absence == null)
                return NotFound();

            return absence;
        }
        [HttpGet("etudiant")]
public async Task<ActionResult<IEnumerable<Absence>>> GetAbsencesEtudiant()
{
    var userIdClaim = User.FindFirst("id")?.Value;
    if (userIdClaim == null)
        return Unauthorized("Utilisateur non authentifié");

    int etudiantId = int.Parse(userIdClaim);

    var absences = await _context.Absences
        .Include(a => a.Matiere)
        .Where(a => a.EtudiantId == etudiantId)
        .ToListAsync();

    return Ok(absences);
}


        // POST: api/Absences
        [HttpPost]
        public async Task<ActionResult<Absence>> CreateAbsence([FromBody] Absence absence)
        {
            _context.Absences.Add(absence);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAbsence), new { id = absence.IdAbsence }, absence);
        }

        // PUT: api/Absences/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAbsence(int id, [FromBody] Absence absence)
        {
            if (id != absence.IdAbsence)
                return BadRequest();

            _context.Entry(absence).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AbsenceExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/Absences/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAbsence(int id)
        {
            var absence = await _context.Absences.FindAsync(id);
            if (absence == null)
                return NotFound();

            _context.Absences.Remove(absence);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AbsenceExists(int id)
        {
            return _context.Absences.Any(a => a.IdAbsence == id);
        }
    }
}
