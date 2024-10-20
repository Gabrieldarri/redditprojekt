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

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        // Endpoint to create a new comment, with postId as a route parameter
        [HttpPost("{postId}")]
        public async Task<IActionResult> CreateComment(int postId, [FromBody] Comment comment)
        {
            // Tjek om kommentaren er null
            if (comment == null || string.IsNullOrWhiteSpace(comment.Content) || string.IsNullOrWhiteSpace(comment.User))
            {
                return BadRequest("Invalid comment data.");
            }

            // Sæt PostId i kommentarobjektet
            comment.PostId = postId; // Sørg for, at din Comment-model har en PostId-egenskab

            // Opret kommentaren via din service
            var createdComment = await _commentService.CreateCommentAsync(comment);

            // Returner den oprettede kommentar med statuskoden 201 Created
            return CreatedAtAction(nameof(CreateComment), new { id = createdComment.Id }, createdComment);
        }



        // Endpoint to upvote a comment
        [HttpPut("{id}/upvote")]
        public async Task<IActionResult> UpvoteComment(int id)
        {
            await _commentService.UpvoteCommentAsync(id);
            return NoContent();
        }

        // Endpoint to downvote a comment
        [HttpPut("{id}/downvote")]
        public async Task<IActionResult> DownvoteComment(int id)
        {
            await _commentService.DownvoteCommentAsync(id);
            return NoContent();
        }
    }
}
