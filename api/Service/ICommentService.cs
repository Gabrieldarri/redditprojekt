using shared.Model;

namespace Service
{
    public interface ICommentService
    {
        Task<Comment> CreateCommentAsync(Comment comment);    // Create a new comment
        Task UpvoteCommentAsync(int id);             // Upvote a comment
        Task DownvoteCommentAsync(int id);           // Downvote a comment
    }

}

