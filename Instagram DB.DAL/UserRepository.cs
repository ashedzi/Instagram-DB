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

        public User GetById(int id) {
            return _context.Users.FirstOrDefault(u => u.CommentId == id);
        }

        public void Add(User)
    }
}
