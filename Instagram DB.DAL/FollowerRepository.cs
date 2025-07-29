using Instagram_DB.Models;

namespace Instagram_DB.DAL {
    public class FollowerRepository {
        private readonly InstagramDbContext _context;

        public FollowerRepository (InstagramDbContext context) {
            _context = context;
        }

        public List<Follower> GetFollowers () {
            return _context.Followers.ToList();
        }
    }
}
