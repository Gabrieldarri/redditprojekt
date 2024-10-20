using shared.Model;
using Data;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class PostService : IPostService
    {
        private readonly AppDbContext _context;

        // Dependency injection af AppDbContext
        public PostService(AppDbContext context)
        {
            _context = context;
        }

        // Henter alle posts inklusiv kommentarer
        public async Task<List<Post>> GetPostsAsync()
        {
            return await _context.Posts.Include(p => p.Comments).ToListAsync();
        }

        // Henter et post baseret på ID, inklusiv kommentarer
        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await _context.Posts.Include(p => p.Comments)
                                       .FirstOrDefaultAsync(p => p.Id == id);
        }

        // Opretter et nyt post i databasen
        public async Task CreatePostAsync(Post post)
        {
            _context.Posts.Add(post); // Tilføjer post
            await _context.SaveChangesAsync(); // Gemmer ændringer
        }

        // Opstemmer et post
        public async Task UpvotePostAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                post.Upvotes++; // Øger opstemmerne
                await _context.SaveChangesAsync(); // Gemmer ændringer
            }
        }

        // Nedstemmer et post
        public async Task DownvotePostAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                post.Downvotes++; // Øger nedstemmerne
                await _context.SaveChangesAsync(); // Gemmer ændringer
            }
        }
    }
}
