using shared.Model;

namespace Service
{
    public interface ICommentService
    {
        Task<Comment> CreateCommentAsync(Comment comment);    
        Task UpvoteCommentAsync(int id);             
        Task DownvoteCommentAsync(int id);           
    }

}

