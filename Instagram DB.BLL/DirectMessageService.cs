using Instagram_DB.DAL;
using Instagram_DB.Models;


namespace Instagram_DB.BLL {
    public class DirectMessageService {
        private readonly DirectMessageRepository _directMessageRepository;

        public DirectMessageService(DirectMessageRepository directMessageRepository) {
            _directMessageRepository = directMessageRepository;
        }

        public List<DirectMessage> GetDirectMessages() {
            return _directMessageRepository.GetDirectMessages();
        }
    }
}
