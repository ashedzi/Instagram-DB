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

    }
}
