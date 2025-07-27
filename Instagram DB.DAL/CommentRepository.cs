using Instagram_DB.Models;

namespace Instagram_DB.DAL {
    public class CommentRepository {
        private readonly InstagramDbContext _context;

        public CommentRepository(InstagramDbContext context) {
            _context = context;
        }

        public List<Comment> GetUsers() {
            return _context.Comments.ToList();
        }
    }
}
