using Model;

namespace Service
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId);
        Task<Comment> CreateCommentAsync(Comment newComment);
        Task UpvoteCommentAsync(int commentId);
        Task DownvoteCommentAsync(int commentId);
        Task<Comment> GetCommentByIdAsync(int id);

    }
}

