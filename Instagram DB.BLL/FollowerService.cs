using Instagram_DB.DAL;
using Instagram_DB.Models;

namespace Instagram_DB.BLL {
    public class FollowerService {
        private readonly FollowerRepository _followerRepository;

        public FollowerService (FollowerRepository followerRepository) {
            _followerRepository = followerRepository;
        }

        public List<Follower> GetFollowers () {
            return _followerRepository.GetFollowers();
        }
    }
}
