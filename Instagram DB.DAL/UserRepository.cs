using Instagram_DB.Models;

namespace Instagram_DB.DAL {
    public class UserRepository {
        private readonly InstagramDbContext _context;
        
        public UserRepository (InstagramDbContext context) {
            _context = context;
        }

        public List<User> GetUsers() {
            return _context.Users.ToList();
        }
    }
}
