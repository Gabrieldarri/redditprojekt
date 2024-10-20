using shared.Model;

namespace Service
{
    public interface IPostService
    {
        Task<List<Post>> GetPostsAsync();           
        Task<Post> GetPostByIdAsync(int id);         
        Task CreatePostAsync(Post post);             
        Task UpvotePostAsync(int id);                
        Task DownvotePostAsync(int id);              
    }


}
