using Instagram_DB.DAL;
using Instagram_DB.Models;


namespace Instagram_DB.BLL {
    public class LikeService {
        private readonly LikeRepository _likeRepository;

        public LikeService(LikeRepository likeRepository) {
            _likeRepository = likeRepository;
        }

        public List<Like> getUsers() {
            return _likeRepository.GetUsers();
        }
    }
}
