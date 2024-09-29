using Model;

namespace Service
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetPostsAsync();
        Task<Post> GetPostByIdAsync(int id);
        Task<Post> CreatePostAsync(Post newPost);
        Task UpvotePostAsync(int id);
        Task DownvotePostAsync(int id);
    }

}
