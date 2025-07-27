using Instagram_DB.Models;

namespace Instagram_DB.DAL {
    internal class CommentRepository {
        private readonly InstagramDbContext _context;

        public CommentRepository(InstagramDbContext context) {
            _context = context;
        }

        public List<User> GetUsers() {
            return _context.Users.ToList();
        }
    }
}
