using Instagram_DB.DAL;
using Instagram_DB.Models;


namespace Instagram_DB.BLL {
    internal class CommentService {
        private readonly UserRepository _userRepository;

        public CommentService(UserRepository userRepository) {
            _userRepository = userRepository;
        }

        public List<User> getUsers() {
            return _userRepository.GetUsers();
        }
    }
}
