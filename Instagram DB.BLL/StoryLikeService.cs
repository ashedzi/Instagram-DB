using Instagram_DB.DAL;
using Instagram_DB.Models;

namespace Instagram_DB.BLL {
    public class StoryLikeService {
        private readonly UserRepository _userRepository;

        public StoryLikeService(UserRepository userRepository) {
            _userRepository = userRepository;
        }

        public List<User> getUsers() {
            return _userRepository.GetUsers();
        }
    }
}
