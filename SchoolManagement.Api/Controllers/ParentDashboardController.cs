using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data;
using System.Security.Claims;

namespace SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("api/parent")]
    [Authorize(Roles = "Parent")]
    public class ParentDashboardController : ControllerBase
    {
        private readonly SchoolContext _context;

        public ParentDashboardController(SchoolContext context)
        {
            _context = context;
        }

        // 1. Retourne TOUS les étudiants (pour tester ton sélecteur React)
        [HttpGet("enfants")]
        public async Task<IActionResult> GetMesEnfants()
        {
            var enfants = await _context.Etudiants
                .Include(e => e.Classe)
                .Select(e => new
                {
                    e.IdEtudiant,
                    e.Nom,
                    e.Prenom,
                    e.Email,
                    NomClasse = e.Classe != null ? e.Classe.NomClasse : "Non assignée"
                })
                .ToListAsync();

            return Ok(enfants);
        }

        // 2. Infos détaillées sans blocage ParentId
        [HttpGet("enfant/{enfantId}/infos")]
        public async Task<IActionResult> GetEnfantInfos(int enfantId)
        {
            var enfant = await _context.Etudiants
                .Include(e => e.Classe)
                .FirstOrDefaultAsync(e => e.IdEtudiant == enfantId);

            if (enfant == null) return NotFound();

            return Ok(new
            {
                enfant.IdEtudiant,
                enfant.Nom,
                enfant.Prenom,
                enfant.Email,
                enfant.DateNaissance,
                NomClasse = enfant.Classe?.NomClasse
            });
        }

        // 3. Emploi du temps
        [HttpGet("enfant/{enfantId}/emploi")]
        public async Task<IActionResult> GetEmploiEnfant(int enfantId)
        {
            var etudiant = await _context.Etudiants.FindAsync(enfantId);
            if (etudiant == null || etudiant.ClasseId == null) return Ok(new List<object>());
            
            var emploi = await _context.EmploisDuTemps
                .Include(e => e.Matiere)
                .Where(e => e.ClasseId == etudiant.ClasseId)
                .OrderBy(e => e.Jour)
                .ToListAsync();

            return Ok(emploi);
        }

        // 4. Absences
        [HttpGet("enfant/{enfantId}/absences")]
        public async Task<IActionResult> GetAbsencesEnfant(int enfantId)
        {
            var absences = await _context.Absences
                .Include(a => a.Matiere)
                .Where(a => a.EtudiantId == enfantId)
                .OrderByDescending(a => a.DateAbsence)
                .ToListAsync();

            return Ok(absences);
        }
    }
}