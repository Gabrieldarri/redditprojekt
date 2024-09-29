using Model;
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

        public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId)
        {
            return await _context.Comments
                .Where(c => c.PostId == postId)
                .ToListAsync();
        }

        public async Task<Comment> CreateCommentAsync(Comment newComment)
        {
            _context.Comments.Add(newComment);
            await _context.SaveChangesAsync();
            return newComment;
        }

        public async Task UpvoteCommentAsync(int commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment != null)
            {
                comment.VoteCount++;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DownvoteCommentAsync(int commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment != null)
            {
                comment.VoteCount--;
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Comment> GetCommentByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

    }
}
