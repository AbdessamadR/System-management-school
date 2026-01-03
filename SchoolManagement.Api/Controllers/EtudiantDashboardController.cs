using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("api/etudiant")]
    [Authorize] // protège toutes les routes
    public class EtudiantDashboardController : ControllerBase
    {
        private readonly SchoolContext _context;

        public EtudiantDashboardController(SchoolContext context)
        {
            _context = context;
        }

        // -------------------------
        // 1️⃣ Infos personnelles
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentEtudiant()
        {
            var userIdClaim = User.FindFirst("id")?.Value;
            if (userIdClaim == null)
                return Unauthorized("Utilisateur non authentifié.");

            int etudiantId = int.Parse(userIdClaim);

            var etudiant = await _context.Etudiants
                .Include(e => e.Classe)
                .Include(e => e.Parent)
                .FirstOrDefaultAsync(e => e.IdEtudiant == etudiantId);

            if (etudiant == null)
                return NotFound("Étudiant non trouvé.");

            return Ok(etudiant);
        }

        // -------------------------
        // 2️⃣ Absences
        [HttpGet("absences")]
        public async Task<IActionResult> GetAbsences()
        {
            var userIdClaim = User.FindFirst("id")?.Value;
            if (userIdClaim == null)
                return Unauthorized("Utilisateur non authentifié.");

            int etudiantId = int.Parse(userIdClaim);

            var absences = await _context.Absences
                .Include(a => a.Matiere)
                .Where(a => a.EtudiantId == etudiantId)
                .OrderByDescending(a => a.DateAbsence)
                .ToListAsync();

            return Ok(absences);
        }

        // -------------------------
        // 3️⃣ Emploi du temps
        [HttpGet("emploi")]
        public async Task<IActionResult> GetEmploiTemps()
        {
            var userIdClaim = User.FindFirst("id")?.Value;
            if (userIdClaim == null)
                return Unauthorized("Utilisateur non authentifié.");

            int etudiantId = int.Parse(userIdClaim);

            var etudiant = await _context.Etudiants.FindAsync(etudiantId);
            if (etudiant == null)
                return NotFound("Étudiant non trouvé.");

            var emploi = await _context.EmploisDuTemps
                .Include(e => e.Matiere)
                .Where(e => e.ClasseId == etudiant.ClasseId)
                .OrderBy(e => e.Jour)
                .ThenBy(e => e.HeureDebut)
                .ToListAsync();

            return Ok(emploi);
        }

        // -------------------------
        // 4️⃣ Notifications
        [HttpGet("notifications")]
        public async Task<IActionResult> GetNotifications()
        {
            var userIdClaim = User.FindFirst("id")?.Value;
            if (userIdClaim == null)
                return Unauthorized("Utilisateur non authentifié.");

            int etudiantId = int.Parse(userIdClaim);

            var etudiant = await _context.Etudiants
                .Include(e => e.Classe)
                .FirstOrDefaultAsync(e => e.IdEtudiant == etudiantId);

            if (etudiant == null)
                return NotFound("Étudiant non trouvé.");

            var notifications = await _context.Notifications
                .Where(n => (n.EtudiantId != null && n.EtudiantId == etudiantId)
                         || (n.ClasseId != null && n.ClasseId == etudiant.ClasseId))
                .OrderByDescending(n => n.DateEnvoi)
                .ToListAsync();

            return Ok(notifications);
        }
    }
}
