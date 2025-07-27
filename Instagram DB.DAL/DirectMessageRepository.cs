using Instagram_DB.Models;

namespace Instagram_DB.DAL {
    internal class DirectMessageRepository {
        private readonly InstagramDbContext _context;

        public DirectMessageRepository(InstagramDbContext context) {
            _context = context;
        }

        public List<User> GetUsers() {
            return _context.Users.ToList();
        }
    }
}
