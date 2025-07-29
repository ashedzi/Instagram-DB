using Instagram_DB.Models;
using Microsoft.EntityFrameworkCore;

namespace Instagram_DB.DAL {
    public class UserRepository {
        private readonly InstagramDbContext _context;
        
        public UserRepository (InstagramDbContext context) {
            _context = context;
        }

        public List<User> GetUsers() {
            return _context.Users.ToList();
        }

        public User? GetUserWithFollowersAndFollowing (string username) {
            return _context.Users
                .Include(u => u.FollowerUsers)
                .Include(u => u.FollowingUsers)
                .FirstOrDefault(u => u.Username.ToLower() == username.ToLower());
        }
    }
}
