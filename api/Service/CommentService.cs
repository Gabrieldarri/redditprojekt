using shared.Model;
using Data;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class CommentService : ICommentService
    {
        private readonly AppDbContext _context;

        public CommentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateCommentAsync(Comment comment)
        {
            // Tilføj logik til at gemme kommentaren i databasen
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return comment; // Returner den oprettede kommentar
        }


        public async Task UpvoteCommentAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment != null)
            {
                comment.Upvotes++;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DownvoteCommentAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment != null)
            {
                comment.Downvotes++;
                await _context.SaveChangesAsync();
            }
        }
    }

}
