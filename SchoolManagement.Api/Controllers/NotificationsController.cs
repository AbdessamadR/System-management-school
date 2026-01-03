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
    public class NotificationsController : ControllerBase
    {
        private readonly SchoolContext _context;

        public NotificationsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: api/admin/Notifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> GetAll()
        {
            var notifications = await _context.Notifications
                .Include(n => n.Destinataire)
                .ToListAsync();
            return Ok(notifications);
        }

        // GET: api/admin/Notifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Notification>> GetOne(int id)
        {
            var notification = await _context.Notifications
                .Include(n => n.Destinataire)
                .FirstOrDefaultAsync(n => n.IdNotification == id);

            if (notification == null)
                return NotFound(new { message = "Notification non trouvée." });

            return Ok(notification);
        }

        // POST: api/admin/Notifications
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Notification notification)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Notification créée avec succès !", notification });
        }

        // PUT: api/admin/Notifications/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Notification notification)
        {
            if (id != notification.IdNotification)
                return BadRequest(new { message = "ID invalide." });

            var existingNotification = await _context.Notifications.FindAsync(id);
            if (existingNotification == null)
                return NotFound(new { message = "Notification non trouvée." });

            // Mettre à jour les champs
            existingNotification.Contenu = notification.Contenu;
            existingNotification.DateEnvoi = notification.DateEnvoi;
            existingNotification.Type = notification.Type;
            existingNotification.AccountId = notification.AccountId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Notifications.Any(e => e.IdNotification == id))
                    return NotFound(new { message = "Notification non trouvée." });
                else
                    throw;
            }

            return Ok(new { message = "Notification mise à jour avec succès !" });
        }

        // DELETE: api/admin/Notifications/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
                return NotFound(new { message = "Notification non trouvée." });

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Notification supprimée avec succès !" });
        }
    }
}
