using shared.Model;
using Data;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class CommentService : ICommentService
    {
        private readonly AppDbContext _context;

        // Dependency injection af AppDbContext
        public CommentService(AppDbContext context)
        {
            _context = context;
        }

        // Opretter en kommentar i databasen
        public async Task<Comment> CreateCommentAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment); // Tilføjer kommentaren
            await _context.SaveChangesAsync(); // Gemmer ændringer

            return comment;
        }

        // Opstemmer en kommentar
        public async Task UpvoteCommentAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment != null)
            {
                comment.Upvotes++; // Øger opstemmerne
                await _context.SaveChangesAsync(); // Gemmer ændringer
            }
        }

        // Nedstemmer en kommentar
        public async Task DownvoteCommentAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment != null)
            {
                comment.Downvotes++; // Øger nedstemmerne
                await _context.SaveChangesAsync(); // Gemmer ændringer
            }
        }
    }
}
