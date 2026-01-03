using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.DTOs;
using System.Threading.Tasks;
using System.Linq;

namespace SchoolManagement.Api.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class ParentsController : ControllerBase
    {
        private readonly SchoolContext _context;

        public ParentsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: api/admin/parents
        [HttpGet]
        public async Task<IActionResult> GetParents()
        {
            var parents = await _context.Parents
                .Include(p => p.Enfants)
                .ToListAsync();

            return Ok(parents);
        }

        // POST: api/admin/parents
        [HttpPost]
        public async Task<IActionResult> CreateParent([FromBody] ParentDTO parentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var parent = new Parent
            {
                Nom = parentDto.Nom,
                Prenom = parentDto.Prenom,
                Email = parentDto.Email,
                Telephone = parentDto.Telephone,
                Adresse = parentDto.Adresse,
                Enfants = await _context.Etudiants
                    .Where(e => parentDto.EtudiantsIds.Contains(e.IdEtudiant))
                    .ToListAsync()
            };

            _context.Parents.Add(parent);
            await _context.SaveChangesAsync();

            // Créer le compte associé
            var compte = new Account
            {
                Username = parent.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(parentDto.MotDePasse),
                Role = "Parent",
                Actif = true
            };
            _context.Accounts.Add(compte);
            await _context.SaveChangesAsync();

            return Ok(parent);
        }

        // PUT: api/admin/parents/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateParent(int id, [FromBody] ParentDTO parentDto)
        {
            var parent = await _context.Parents
                .Include(p => p.Enfants)
                .FirstOrDefaultAsync(p => p.IdParent == id);

            if (parent == null)
                return NotFound();

            parent.Nom = parentDto.Nom;
            parent.Prenom = parentDto.Prenom;
            parent.Email = parentDto.Email;
            parent.Telephone = parentDto.Telephone;
            parent.Adresse = parentDto.Adresse;

            // Mettre à jour les enfants
            if (parentDto.EtudiantsIds != null)
            {
                parent.Enfants = await _context.Etudiants
                    .Where(e => parentDto.EtudiantsIds.Contains(e.IdEtudiant))
                    .ToListAsync();
            }

            // Mettre à jour le mot de passe si fourni
            if (!string.IsNullOrEmpty(parentDto.MotDePasse))
            {
                var compte = await _context.Accounts.FirstOrDefaultAsync(a => a.Username == parent.Email);
                if (compte != null)
                    compte.PasswordHash = BCrypt.Net.BCrypt.HashPassword(parentDto.MotDePasse);
            }

            await _context.SaveChangesAsync();
            return Ok(parent);
        }

        // DELETE: api/admin/parents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParent(int id)
        {
            var parent = await _context.Parents.FindAsync(id);
            if (parent == null)
                return NotFound();

            var compte = await _context.Accounts.FirstOrDefaultAsync(a => a.Username == parent.Email);
            if (compte != null)
                _context.Accounts.Remove(compte);

            _context.Parents.Remove(parent);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
