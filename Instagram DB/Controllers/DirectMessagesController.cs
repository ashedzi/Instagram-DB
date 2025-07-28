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
        public IActionResult SendMessage(int? userId) {
            if (userId == null) {
                return BadRequest("User ID is required.");
            }

            ViewBag.UserId = userId;
            ViewBag.Users = _context.Users
                .Select(u => new { u.UserId, u.Username })
                .ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(int userId, int receiverUserId, string textContent) {
            if (ModelState.IsValid) {
                var message = new DirectMessage {
                    SenderUserId = userId,
                    ReceiverUserId = receiverUserId,
                    TextContent = textContent,
                    Timestamp = DateTime.Now
                };

                _context.DirectMessages.Add(message);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { userId });
            }

            ViewBag.UserId = userId;
            ViewBag.Users = _context.Users
                .Select(u => new { u.UserId, u.Username })
                .ToList();
            return View();
        }
    }
}