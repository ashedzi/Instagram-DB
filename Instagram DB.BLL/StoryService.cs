using Instagram_DB.DAL;
using Instagram_DB.Models;


namespace Instagram_DB.BLL {
    public class StoryService {
        private readonly UserRepository _userRepository;

        public StoryService(UserRepository userRepository) {
            _userRepository = userRepository;
        }

        public List<User> getUsers() {
            return _userRepository.GetUsers();
        }
    }
}
