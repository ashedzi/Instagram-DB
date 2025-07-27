using Instagram_DB.DAL;
using Instagram_DB.Models;


namespace Instagram_DB.BLL {
    public class StoryService {
        private readonly StoryRepository _storyRepository;

        public StoryService(StoryRepository storyRepository) {
            _storyRepository = storyRepository;
        }

        public List<Story> getUsers() {
            return _storyRepository.GetUsers();
        }
    }
}
