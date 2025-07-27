using Instagram_DB.Models;

namespace Instagram_DB.DAL {
    public class DirectMessageRepository {
        private readonly InstagramDbContext _context;

        public DirectMessageRepository(InstagramDbContext context) {
            _context = context;
        }

        public List<DirectMessage> GetUsers() {
            return _context.DirectMessages.ToList();
        }
    }
}
