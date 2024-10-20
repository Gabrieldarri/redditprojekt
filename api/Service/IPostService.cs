using shared.Model;

namespace Service
{
    public interface IPostService
    {
        Task<List<Post>> GetPostsAsync();            // Fetch all posts
        Task<Post> GetPostByIdAsync(int id);         // Fetch a single post by ID
        Task CreatePostAsync(Post post);             // Create a new post
        Task UpvotePostAsync(int id);                // Upvote a post
        Task DownvotePostAsync(int id);              // Downvote a post
    }


}
