using System.Net.Http.Json;
using System.Text.Json;
using shared.Model;

namespace redditpages.Data;

public class ApiService
{
    private readonly HttpClient http;
    private readonly string baseAPI = "https://localhost:7225/api/";

    public ApiService(HttpClient http)
    {
        this.http = http;
    }

    public async Task<Post[]> GetPosts()
    {
        string url = $"{baseAPI}post";
        try
        {
            return await http.GetFromJsonAsync<Post[]>(url);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error fetching posts: {ex.Message}");
            return Array.Empty<Post>(); // Returner tom array ved fejl
        }
    }

    public async Task<Post> GetPost(int id)
    {
        string url = $"{baseAPI}post/{id}";
        try
        {
            return await http.GetFromJsonAsync<Post>(url);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error fetching post with ID {id}: {ex.Message}");
            return null; // Returner null ved fejl
        }
    }

    public async Task<Post> CreatePost(string title, string content, string user)
    {
        string url = $"{baseAPI}post"; // URL til din API
        var postData = new Post { Title = title, Content = content, User = user }; // Opret Post objektet

        try
        {
            HttpResponseMessage msg = await http.PostAsJsonAsync(url, postData);
            msg.EnsureSuccessStatusCode();

            string json = await msg.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Post>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error creating post: {ex.Message}");
            return null; // Returner null ved fejl
        }
    }

    public async Task<Comment> CreateComment(string content, string user, int postId)
    {
        string url = $"{baseAPI}comment/{postId}";
        var commentData = new Comment { Content = content, User = user, PostId = postId }; // Opret kommentarobjektet

        try
        {
            HttpResponseMessage msg = await http.PostAsJsonAsync(url, commentData);
            msg.EnsureSuccessStatusCode(); // Kaster en undtagelse, hvis statuskode ikke er 2xx

            string json = await msg.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Comment>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error creating comment: {ex.Message}");
            return null; // Returner null ved fejl
        }
    }



    public async Task UpvotePost(int id)
    {
        string url = $"{baseAPI}post/{id}/upvote";
        try
        {
            HttpResponseMessage msg = await http.PutAsync(url, null);
            msg.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error upvoting post with ID {id}: {ex.Message}");
        }
    }

    public async Task DownvotePost(int id)
    {
        string url = $"{baseAPI}post/{id}/downvote";
        try
        {
            HttpResponseMessage msg = await http.PutAsync(url, null);
            msg.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error downvoting post with ID {id}: {ex.Message}");
        }
    }

    public async Task UpvoteComment(int id)
    {
        string url = $"{baseAPI}comment/{id}/upvote";
        try
        {
            HttpResponseMessage msg = await http.PutAsync(url, null);
            msg.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error upvoting comment with ID {id}: {ex.Message}");
        }
    }

    public async Task DownvoteComment(int id)
    {
        string url = $"{baseAPI}comment/{id}/downvote";
        try
        {
            HttpResponseMessage msg = await http.PutAsync(url, null);
            msg.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error downvoting comment with ID {id}: {ex.Message}");
        }
    }
}
