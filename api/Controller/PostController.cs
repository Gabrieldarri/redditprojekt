using Microsoft.AspNetCore.Mvc;
using shared.Model;
using Service;

namespace Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        // Dependency injection af post service
        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        // Henter alle posts
        [HttpGet]
        public async Task<ActionResult<List<Post>>> GetPosts()
        {
            var posts = await _postService.GetPostsAsync();
            return Ok(posts);
        }

        // Henter et enkelt post baseret på ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        // Opretter en ny post
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] Post post)
        {
            // Validerer post-data
            if (post == null || string.IsNullOrWhiteSpace(post.User))
            {
                return BadRequest("Post cannot be null and User is required.");
            }

            await _postService.CreatePostAsync(post);

            // Returnerer den oprettede post
            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }

        // Upvote en post
        [HttpPut("{id}/upvote")]
        public async Task<IActionResult> UpvotePost(int id)
        {
            await _postService.UpvotePostAsync(id);
            return NoContent();
        }

        // Downvote en post
        [HttpPut("{id}/downvote")]
        public async Task<IActionResult> DownvotePost(int id)
        {
            await _postService.DownvotePostAsync(id);
            return NoContent();
        }
    }
}
