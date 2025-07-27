using Instagram_DB.Models;

namespace Instagram_DB.DAL {
    public class StoryLikeRepository {
        private readonly InstagramDbContext _context;

        public StoryLikeRepository(InstagramDbContext context) {
            _context = context;
        }

        public List<StoryLike> GetUsers() {
            return _context.StoryLikes.ToList();
        }
    }
}