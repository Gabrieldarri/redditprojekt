using Microsoft.AspNetCore.Mvc;
using shared.Model;
using Service;

namespace Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        // Dependency injection af comment service
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        // Endpoint for at oprette en ny kommentar
        [HttpPost("{postId}")]
        public async Task<IActionResult> CreateComment(int postId, [FromBody] Comment comment)
        {
            // Tjekker om kommentaren er gyldig
            if (comment == null || string.IsNullOrWhiteSpace(comment.Content) || string.IsNullOrWhiteSpace(comment.User))
            {
                return BadRequest("Invalid comment data.");
            }

            // Tilknyt postId til kommentaren
            comment.PostId = postId;

            // Opretter kommentar via service og returnerer resultatet
            var createdComment = await _commentService.CreateCommentAsync(comment);
            return CreatedAtAction(nameof(CreateComment), new { id = createdComment.Id }, createdComment);
        }

        // Endpoint for at upvote en kommentar
        [HttpPut("{id}/upvote")]
        public async Task<IActionResult> UpvoteComment(int id)
        {
            await _commentService.UpvoteCommentAsync(id);
            return NoContent();
        }

        // Endpoint for at downvote en kommentar
        [HttpPut("{id}/downvote")]
        public async Task<IActionResult> DownvoteComment(int id)
        {
            await _commentService.DownvoteCommentAsync(id);
            return NoContent();
        }
    }
}
