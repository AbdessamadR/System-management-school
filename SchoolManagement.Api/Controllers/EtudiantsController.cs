using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("api/admin/etudiants")]
    public class EtudiantsController : ControllerBase
    {
        private readonly SchoolContext _context;

        public EtudiantsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: api/admin/etudiants
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _context.Etudiants
                .Include(e => e.Classe)
                .Include(e => e.Parent)
                .ToListAsync();
            return Ok(students);
        }

        // GET: api/admin/etudiants/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await _context.Etudiants
                .Include(e => e.Classe)
                .Include(e => e.Parent)
                .FirstOrDefaultAsync(e => e.IdEtudiant == id);

            if (student == null) return NotFound();
            return Ok(student);
        }

        // POST: api/admin/etudiants
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStudentRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // 🛡️ Utilisation d'une transaction pour garantir l'intégrité des données
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var student = new Etudiant
                {
                    Nom = request.Nom,
                    Prenom = request.Prenom,
                    DateNaissance = request.DateNaissance,
                    Email = request.Email,
                    ClasseId = request.ClasseId,
                    ParentId = request.ParentId
                };

                _context.Etudiants.Add(student);
                await _context.SaveChangesAsync(); // Sauvegarde pour obtenir l'ID de l'étudiant

                var account = new Account
                {
                    Username = request.Email,
                    // Utilisation de BCrypt pour le hashage du mot de passe
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    Role = "Etudiant",
                    EtudiantId = student.IdEtudiant
                };

                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return Ok(new { message = "Étudiant et compte créés avec succès !", id = student.IdEtudiant });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, $"Erreur lors de la création : {ex.Message}");
            }
        }

        // PUT: api/admin/etudiants/5
        // Le ":int" force le routage correct et évite l'erreur 405
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateStudentRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var student = await _context.Etudiants.FindAsync(id);
            if (student == null) return NotFound(new { message = "Étudiant non trouvé." });

            student.Nom = request.Nom;
            student.Prenom = request.Prenom;
            student.Email = request.Email;
            student.DateNaissance = request.DateNaissance;
            student.ClasseId = request.ClasseId;
            student.ParentId = request.ParentId;

            // Mise à jour optionnelle du compte (mot de passe ou username)
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.EtudiantId == id);
            if (account != null)
            {
                account.Username = request.Email;
                if (!string.IsNullOrEmpty(request.Password))
                {
                    account.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
                }
            }

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Dossier étudiant mis à jour." });
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Erreur de concurrence lors de la mise à jour.");
            }
        }

        // DELETE: api/admin/etudiants/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Etudiants.FindAsync(id);
            if (student == null) return NotFound();

            // 🛡️ Supprimer d'abord le compte pour respecter les contraintes de clé étrangère
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.EtudiantId == id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
            }

            _context.Etudiants.Remove(student);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Étudiant et compte supprimés avec succès." });
        }
    }

    // --- Data Transfer Objects (DTOs) ---

    public class CreateStudentRequest
    {
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime DateNaissance { get; set; }
        public int ClasseId { get; set; }
        public int? ParentId { get; set; }
    }

    public class UpdateStudentRequest
    {
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Password { get; set; } 
        public DateTime DateNaissance { get; set; }
        public int ClasseId { get; set; }
        public int? ParentId { get; set; }
    }
}