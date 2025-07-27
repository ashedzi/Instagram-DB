using Instagram_DB.DAL;
using Instagram_DB.Models;


namespace Instagram_DB.BLL {
    public class PostService {
        private readonly PostRepository _postRepository;

        public PostService(PostRepository postRepository) {
            _postRepository = postRepository;
        }

        public List<Post> getUsers() {
            return _postRepository.GetUsers();
        }
    }
}
