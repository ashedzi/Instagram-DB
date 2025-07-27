using Instagram_DB.Models;

namespace Instagram_DB.DAL {
    public class PostRepository {
        private readonly InstagramDbContext _context;

        public PostRepository(InstagramDbContext context) {
            _context = context;
        }

        public List<Post> GetUsers() {
            return _context.Posts.ToList();
        }
    }
}
