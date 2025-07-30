using Instagram_DB.DAL;
using Instagram_DB.Models;

namespace Instagram_DB.BLL {
    public class UserService {
        private readonly UserRepository _userRepository;

        public UserService (UserRepository userRepository) {
            _userRepository = userRepository;
        }

        public List<User> GetUsers () {
            return _userRepository.GetUsers();
        }

        public User? GetUserWithFollowersAndFollowing (string username) {
            return _userRepository.GetUserWithFollowersAndFollowing(username);
        }
    }
}
