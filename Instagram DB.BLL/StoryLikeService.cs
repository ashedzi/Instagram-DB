using Instagram_DB.DAL;
using Instagram_DB.Models;

namespace Instagram_DB.BLL {
    public class StoryLikeService {
        private readonly StoryLikeRepository _storyLikeRepository;

        public StoryLikeService(StoryLikeRepository storyLikeRepository) {
            _storyLikeRepository = storyLikeRepository;
        }

        public List<StoryLike> GetStoryLikes() {
            return _storyLikeRepository.GetStoryLikes();
        }
    }
}
