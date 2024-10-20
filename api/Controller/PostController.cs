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

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Post>>> GetPosts()
        {
            var posts = await _postService.GetPostsAsync();
            return Ok(posts);
        }

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

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] Post post)
        {
            if (post == null || string.IsNullOrWhiteSpace(post.User))
            {
                return BadRequest("Post cannot be null and User is required.");
            }

            await _postService.CreatePostAsync(post);

            // Returner den oprettede post
            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
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
    }

}
