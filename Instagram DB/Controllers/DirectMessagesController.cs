using Instagram_DB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram_DB.Controllers {
    public class DirectMessagesController : Controller {
        private readonly InstagramDbContext _context;

        public DirectMessagesController(InstagramDbContext context) {
            _context = context;
        }

        public async Task<IActionResult> Index(int? userId) {
            if (userId == null) {
                return BadRequest("User ID is required.");
            }

            var messages = await _context.DirectMessages
                .Include(dm => dm.SenderUser)
                .Include(dm => dm.ReceiverUser)
                .Where(dm => dm.SenderUserId == userId || dm.ReceiverUserId == userId)
                .OrderByDescending(dm => dm.Timestamp)
                .ToListAsync();

            ViewBag.UserId = userId;
            return View(messages);
        }
    }
}