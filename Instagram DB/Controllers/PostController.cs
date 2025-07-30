using Instagram_DB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram_DB.Controllers {
    public class PostController : Controller {
        private readonly InstagramDbContext _context;

        public PostController(InstagramDbContext context) {
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

            int currentUserId = 1;
            ViewBag.CurrentUserId = currentUserId;

            bool userHasLiked = await _context.Likes
                .AnyAsync(l => l.PostId == id && l.LikerUserId == currentUserId);

            ViewBag.UserHasLiked = userHasLiked;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(string postId, string content, int userId) {
            if (string.IsNullOrWhiteSpace(postId) || string.IsNullOrWhiteSpace(content)) {
                return RedirectToAction("Details", new { id = postId });
            }

            var post = await _context.Posts.FindAsync(postId);
            if (post == null) {
                return NotFound("Post not found.");
            }

            var userExists = await _context.Users.AnyAsync(u => u.UserId == userId);
            if (!userExists) {
                return NotFound("Commenting user not found.");
            }

            var newComment = new Comment {
                PostId = postId,
                CommenterUserId = userId,
                PosterUserId = post.UserId,
                Content = content,
                Likes = 0,
                Timestamp = DateTime.Now
            };

            _context.Comments.Add(newComment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = postId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLike(string postId, int userId) {
            if (string.IsNullOrWhiteSpace(postId))
                return BadRequest("Invalid post ID.");

            var post = await _context.Posts.FindAsync(postId);
            if (post == null)
                return NotFound("Post not found.");

            var existingLike = await _context.Likes
                .FirstOrDefaultAsync(l => l.PostId == postId && l.LikerUserId == userId);

            if (existingLike != null) {
                _context.Likes.Remove(existingLike);
                if (post.Likes > 0)
                    post.Likes--;
            } else {
                var newLike = new Like {
                    PostId = postId,
                    PosterUserId = post.UserId,
                    LikerUserId = userId
                };

                _context.Likes.Add(newLike);
                post.Likes++;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = postId });
        }

        public async Task<IActionResult> Delete(string id) {
            if (string.IsNullOrEmpty(id)) {
                return BadRequest("Post ID is required.");
            }

            var post = await _context.Posts
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.PostId == id);

            if (post == null) {
                return NotFound("Post not found.");
            }

            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id) {
            var post = await _context.Posts.FindAsync(id);
            if (post == null) {
                return NotFound("Post not found.");
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { userId = post.UserId });
        }

    }
}
