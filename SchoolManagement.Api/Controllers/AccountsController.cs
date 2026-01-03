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
    public class AccountsController : ControllerBase
    {
        private readonly SchoolContext _context;

        public AccountsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: api/admin/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAll()
        {
            var accounts = await _context.Accounts
                .Include(a => a.Etudiant)
                .Include(a => a.Professeur)
                .Include(a => a.Parent)
                .Include(a => a.Administrateur)
                .ToListAsync();

            return Ok(accounts);
        }

        // GET: api/admin/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetOne(int id)
        {
            var account = await _context.Accounts
                .Include(a => a.Etudiant)
                .Include(a => a.Professeur)
                .Include(a => a.Parent)
                .Include(a => a.Administrateur)
                .FirstOrDefaultAsync(a => a.IdAccount == id);

            if (account == null)
                return NotFound(new { message = "Compte non trouvé." });

            return Ok(account);
        }

        // POST: api/admin/Accounts
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Account account)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Compte créé avec succès !", account });
        }

        // PUT: api/admin/Accounts/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Account account)
        {
            if (id != account.IdAccount)
                return BadRequest(new { message = "ID invalide." });

            var existingAccount = await _context.Accounts.FindAsync(id);
            if (existingAccount == null)
                return NotFound(new { message = "Compte non trouvé." });

            // Mise à jour des champs (à adapter selon ton modèle)
            existingAccount.Username = account.Username;
            existingAccount.PasswordHash = account.PasswordHash;
            existingAccount.Role = account.Role;
            existingAccount.EtudiantId = account.EtudiantId;
            existingAccount.ProfesseurId = account.ProfesseurId;
            existingAccount.ParentId = account.ParentId;
            existingAccount.AdministrateurId = account.AdministrateurId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Accounts.Any(a => a.IdAccount == id))
                    return NotFound(new { message = "Compte non trouvé." });
                else
                    throw;
            }

            return Ok(new { message = "Compte mis à jour avec succès !" });
        }

        // DELETE: api/admin/Accounts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
                return NotFound(new { message = "Compte non trouvé." });

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Compte supprimé avec succès !" });
        }
    }
}
