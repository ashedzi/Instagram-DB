using Instagram_DB.Models;

namespace Instagram_DB.DAL {
    internal class PostRepository {
        private readonly InstagramDbContext _context;

        public PostRepository(InstagramDbContext context) {
            _context = context;
        }

        public List<User> GetUsers() {
            return _context.Users.ToList();
        }
    }
}
