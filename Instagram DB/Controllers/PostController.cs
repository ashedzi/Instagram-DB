using Instagram_DB.BLL;
using Instagram_DB.DAL;
using Instagram_DB.Models;
using Microsoft.AspNetCore.Mvc;

namespace Instagram_DB.Controllers {
    public class PostController : Controller {

        private readonly PostService _postService;
        private readonly InstagramDbContext _context;

        public PostController() {
            _context = new InstagramDbContext();
            PostRepository postRepository = new PostRepository(_context);
            _postService = new PostService(postRepository);
        }

        public IActionResult Index() {
            List<Post> posts = _postService.GetPosts();
            return View(posts);
        }

        public IActionResult Details(string id) {
            if (String.IsNullOrEmpty(id)) {
                return BadRequest();
            }

            Post post = _postService.GetPosts()
                .FirstOrDefault(p => p.PostId == id);

            if (post == null) {
                return NotFound();
            }

            List<Comment> comments = _context.Comments
                .Where(c => c.PostId == id)
                .OrderByDescending(c => c.Timestamp)
                .ToList();

            List<Like> likes = _context.Likes
                .Where(l => l.PostId == id)
                .ToList();

            ViewBag.Comments = comments;
            ViewBag.Likes = likes;

            return View(post);
        }
    }
}
