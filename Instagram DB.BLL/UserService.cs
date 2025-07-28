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

        public void Add(User user) {
            _userRepository.Add(user);
        }

        public void Update(User user) {
            _userRepository.Update(user);
        }
        public void Delete(int id) {
            _userRepository.Delete(id);
        }
    }
}
