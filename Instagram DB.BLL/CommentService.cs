using Instagram_DB.DAL;
using Instagram_DB.Models;


namespace Instagram_DB.BLL {
    public class CommentService {
        private readonly CommentRepository _commentRepository;

        public CommentService(CommentRepository commentRepository) {
            _commentRepository = commentRepository;
        }

        public List<Comment> GetComments() {
            return _commentRepository.GetComments();
        }
    }
}
