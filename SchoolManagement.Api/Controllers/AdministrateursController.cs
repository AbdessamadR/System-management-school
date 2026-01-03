using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Api.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class AdministrateursController : ControllerBase
    {
        private readonly SchoolContext _context;

        public AdministrateursController(SchoolContext context)
        {
            _context = context;
        }

        // GET: api/admin/Administrateurs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Administrateur>>> GetAll()
        {
            var admins = await _context.Administrateurs
                .Include(a => a.Account)
                .ToListAsync();
            return Ok(admins);
        }

        // GET: api/admin/Administrateurs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Administrateur>> GetOne(int id)
        {
            var admin = await _context.Administrateurs
                .Include(a => a.Account)
                .FirstOrDefaultAsync(a => a.IdAdmin == id);

            if (admin == null)
                return NotFound(new { message = "Administrateur non trouvé." });

            return Ok(admin);
        }

        // POST: api/admin/Administrateurs
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Administrateur administrateur)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Administrateurs.Add(administrateur);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Administrateur créé avec succès !", administrateur });
        }

        // PUT: api/admin/Administrateurs/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Administrateur administrateur)
        {
            if (id != administrateur.IdAdmin)
                return BadRequest(new { message = "ID invalide." });

            var existingAdmin = await _context.Administrateurs.FindAsync(id);
            if (existingAdmin == null)
                return NotFound(new { message = "Administrateur non trouvé." });

            // Mise à jour des champs
            existingAdmin.Nom = administrateur.Nom;
            existingAdmin.Email = administrateur.Email;
            existingAdmin.Role = administrateur.Role;
            existingAdmin.AccountId = administrateur.AccountId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Administrateurs.Any(e => e.IdAdmin == id))
                    return NotFound(new { message = "Administrateur non trouvé." });
                else
                    throw;
            }

            return Ok(new { message = "Administrateur mis à jour avec succès !" });
        }

        // DELETE: api/admin/Administrateurs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var admin = await _context.Administrateurs.FindAsync(id);
            if (admin == null)
                return NotFound(new { message = "Administrateur non trouvé." });

            _context.Administrateurs.Remove(admin);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Administrateur supprimé avec succès !" });
        }
    }
}
