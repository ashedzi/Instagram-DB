using Instagram_DB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram_DB.Controllers {
    public class PostsController : Controller {
        private readonly InstagramDbContext _context;

        public PostsController(InstagramDbContext context) {
            _context = context;
        }

        public async Task<IActionResult> Index(int? userId) {
            var posts = _context.Posts
                .Include(p => p.User)
                .Include(p => p.Comments)
                .OrderByDescending(p => p.Timestamp)
                .AsQueryable();

            if (userId != null) {
                posts = posts.Where(p => p.UserId == userId);
                ViewBag.UserId = userId;
            }

            return View(await posts.ToListAsync());
        }

        public async Task<IActionResult> Details(string id) {
            if (string.IsNullOrEmpty(id)) {
                return BadRequest("Post ID is required.");
            }

            var post = await _context.Posts
                .Include(p => p.User)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.CommenterUser)
                .FirstOrDefaultAsync(p => p.PostId == id);

            if (post == null) {
                return NotFound("Post not found.");
            }

            return View(post);
        }

    }
}
