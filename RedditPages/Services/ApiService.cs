using System.Net.Http.Json;
using System.Text.Json;
using shared.Model;

namespace redditpages.Data;

public class ApiService
{
    private readonly HttpClient http; // HttpClient til at udføre HTTP-anmodninger
    private readonly string baseAPI = "http://localhost:5177/api/"; // Basis-URL til API'et

    public ApiService(HttpClient http)
    {
        this.http = http; // Initialiserer HttpClient
    }

    // Henter alle posts
    public async Task<Post[]> GetPosts()
    {
        string url = $"{baseAPI}post"; // URL til posts
        try
        {
            return await http.GetFromJsonAsync<Post[]>(url); // Henter posts som JSON
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error fetching posts: {ex.Message}"); // Logger fejl
            return Array.Empty<Post>(); // Returnerer en tom liste ved fejl
        }
    }

    // Henter en post baseret på ID
    public async Task<Post> GetPost(int id)
    {
        string url = $"{baseAPI}post/{id}"; // URL til specifik post
        try
        {
            return await http.GetFromJsonAsync<Post>(url); // Henter post som JSON
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error fetching post with ID {id}: {ex.Message}"); // Logger fejl
            return null; // Returnerer null ved fejl
        }
    }

    // Opretter en ny post
    public async Task<Post> CreatePost(string title, string content, string user)
    {
        string url = $"{baseAPI}post"; // URL til oprettelse af post
        var postData = new Post { Title = title, Content = content, User = user }; // Opretter ny post-data

        try
        {
            HttpResponseMessage msg = await http.PostAsJsonAsync(url, postData); // Sender post-data som JSON
            msg.EnsureSuccessStatusCode(); // Tjekker om anmodningen var succesfuld

            string json = await msg.Content.ReadAsStringAsync(); // Læser JSON-svar
            return JsonSerializer.Deserialize<Post>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Muliggør case-insensitive deserialisering
            });
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error creating post: {ex.Message}"); // Logger fejl
            return null; // Returnerer null ved fejl
        }
    }

    // Opretter en kommentar til en post
    public async Task<Comment> CreateComment(string content, string user, int postId)
    {
        string url = $"{baseAPI}comment/{postId}"; // URL til oprettelse af kommentar
        var commentData = new Comment { Content = content, User = user, PostId = postId }; // Opretter ny kommentar-data

        try
        {
            HttpResponseMessage msg = await http.PostAsJsonAsync(url, commentData); // Sender kommentar-data som JSON
            msg.EnsureSuccessStatusCode(); // Tjekker om anmodningen var succesfuld

            string json = await msg.Content.ReadAsStringAsync(); // Læser JSON-svar
            return JsonSerializer.Deserialize<Comment>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Muliggør case-insensitive deserialisering
            });
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error creating comment: {ex.Message}"); // Logger fejl
            return null; // Returnerer null ved fejl
        }
    }

    // Opstemmer en post
    public async Task UpvotePost(int id)
    {
        string url = $"{baseAPI}post/{id}/upvote"; // URL til opstemning af post
        try
        {
            HttpResponseMessage msg = await http.PutAsync(url, null); // Sender PUT-anmodning
            msg.EnsureSuccessStatusCode(); // Tjekker om anmodningen var succesfuld
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error upvoting post with ID {id}: {ex.Message}"); // Logger fejl
        }
    }

    // Nedstemmer en post
    public async Task DownvotePost(int id)
    {
        string url = $"{baseAPI}post/{id}/downvote"; // URL til nedstemning af post
        try
        {
            HttpResponseMessage msg = await http.PutAsync(url, null); // Sender PUT-anmodning
            msg.EnsureSuccessStatusCode(); // Tjekker om anmodningen var succesfuld
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error downvoting post with ID {id}: {ex.Message}"); // Logger fejl
        }
    }

    // Opstemmer en kommentar
    public async Task UpvoteComment(int id)
    {
        string url = $"{baseAPI}comment/{id}/upvote"; // URL til opstemning af kommentar
        try
        {
            HttpResponseMessage msg = await http.PutAsync(url, null); // Sender PUT-anmodning
            msg.EnsureSuccessStatusCode(); // Tjekker om anmodningen var succesfuld
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error upvoting comment with ID {id}: {ex.Message}"); // Logger fejl
        }
    }

    // Nedstemmer en kommentar
    public async Task DownvoteComment(int id)
    {
        string url = $"{baseAPI}comment/{id}/downvote"; // URL til nedstemning af kommentar
        try
        {
            HttpResponseMessage msg = await http.PutAsync(url, null); // Sender PUT-anmodning
            msg.EnsureSuccessStatusCode(); // Tjekker om anmodningen var succesfuld
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error downvoting comment with ID {id}: {ex.Message}"); // Logger fejl
        }
    }
}
