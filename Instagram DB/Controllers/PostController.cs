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
    }
}
