using Microsoft.AspNetCore.Mvc;
using Model;
using Service;

namespace Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;

        public PostController(IPostService postService, ICommentService commentService)
        {
            _postService = postService;
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postService.GetPostsAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
                return NotFound();
            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(Post post)
        {
            var newPost = await _postService.CreatePostAsync(post);
            return CreatedAtAction(nameof(GetPostById), new { id = newPost.Id }, newPost);
        }

        [HttpPut("{id}/upvote")]
        public async Task<IActionResult> UpvotePost(int id)
        {
            await _postService.UpvotePostAsync(id);
            return NoContent();
        }

        [HttpPut("{id}/downvote")]
        public async Task<IActionResult> DownvotePost(int id)
        {
            await _postService.DownvotePostAsync(id);
            return NoContent();
        }

        [HttpPost("{postId}/comments")]
        public async Task<IActionResult> CreateComment(int postId, [FromBody] Comment comment)
        {
            // Set the PostId from the route
            comment.PostId = postId;

            // Create the comment
            var newComment = await _commentService.CreateCommentAsync(comment);
            return CreatedAtAction(nameof(GetCommentById), new { id = newComment.Id }, newComment);
        }


        [HttpGet("{postId}/comments")]
        public async Task<IActionResult> GetCommentsByPostId(int postId)
        {
            var comments = await _commentService.GetCommentsByPostIdAsync(postId);
            return Ok(comments);
        }

        [HttpGet("comments/{id}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            if (comment == null)
                return NotFound();
            return Ok(comment);
        }
    }
}
