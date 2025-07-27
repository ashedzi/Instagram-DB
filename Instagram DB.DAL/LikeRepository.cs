using Instagram_DB.Models;

namespace Instagram_DB.DAL {
    public class LikeRepository {
        private readonly InstagramDbContext _context;

        public LikeRepository(InstagramDbContext context) {
            _context = context;
        }

        public List<User> GetUsers() {
            return _context.Users.ToList();
        }
    }
}
