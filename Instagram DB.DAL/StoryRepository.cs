using Instagram_DB.Models;

namespace Instagram_DB.DAL {
    public class StoryRepository {
        private readonly InstagramDbContext _context;

        public StoryRepository(InstagramDbContext context) {
            _context = context;
        }

        public List<Story> GetStories() {
            return _context.Stories.ToList();
        }
    }
}
