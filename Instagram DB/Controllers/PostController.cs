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

        public IActionResult Create(int? userId) {
            if (userId == null) {
                return BadRequest("User ID is required.");
            }

            ViewBag.UserId = userId;
            return View();
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string image, string? caption, int userId) {
            if (string.IsNullOrWhiteSpace(image)) {
                ModelState.AddModelError("", "Image URL is required.");
            }

            if (ModelState.IsValid) {
                var newPost = new Post {
                    PostId = Guid.NewGuid().ToString("N").Substring(0, 12).ToUpper(),
                    Image = image,
                    Caption = caption,
                    Timestamp = DateTime.Now,
                    Likes = 0,
                    Saves = 0,
                    UserId = userId
                };

                _context.Posts.Add(newPost);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { userId });
            }

            ViewBag.UserId = userId;
            return View();
        }

    }
}
