using System.Text;
using System.Text.Json;

namespace Assignment2.Data; 

public class PostsService {
    private readonly HttpClientHandler _handler;
    private const string BasePath = "https://localhost:7173/Posts";

    public PostsService() {
        // Trusting all certificates
        //https://stackoverflow.com/a/46626858
        _handler = new HttpClientHandler();
        _handler.ClientCertificateOptions = ClientCertificateOption.Manual;
        _handler.ServerCertificateCustomValidationCallback =
            (httpRequestMessage, cert, cetChain, policyErrors) => {
                return true; 
            };
    }

    public async Task<Post> CreatePost(Post submittedPost) {
        // executing request
        using HttpClient client = new HttpClient(_handler);
        string postAsJson = JsonSerializer.Serialize(submittedPost);
        var requestContent = new StringContent(postAsJson, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(BasePath, requestContent);
        
        // handling error responses
        if (!response.IsSuccessStatusCode) {
            Console.WriteLine("Error while creating a post!");
            Console.WriteLine("JSON request: "+postAsJson);
            Console.WriteLine("Error code: "+response.StatusCode);
            Console.WriteLine("Error message: "+ await response.Content.ReadAsStringAsync());
            throw new Exception($@"Failed to create a post! Error: {await response.Content.ReadAsStringAsync()}");
        }

        // handling successful responses
        var responseJsonString = await response.Content.ReadAsStringAsync();
        var returnedPost = JsonSerializer.Deserialize<Post>(responseJsonString);

        if (returnedPost == null)
            throw new JsonException("Couldn't parse returned post!");
        
        return returnedPost;
    }

    public async Task<List<Post>> GetAllPosts() {
        using HttpClient client = new HttpClient(_handler);
        var response = await client.GetAsync(BasePath);
        
        // handling error responses
        if (!response.IsSuccessStatusCode) {
            Console.WriteLine("Error while getting all posts!");
            Console.WriteLine("Error code: "+response.StatusCode);
            Console.WriteLine("Error message: "+ await response.Content.ReadAsStringAsync());
            throw new Exception($@"Failed to get all posts! Error: {await response.Content.ReadAsStringAsync()}");
        }

        // handling successful responses
        var responseJsonString = await response.Content.ReadAsStringAsync();
        var returnedPosts = JsonSerializer.Deserialize<List<Post>>(responseJsonString);

        if (returnedPosts == null)
            throw new JsonException("Couldn't parse returned post list!");

        return returnedPosts;
    }
    
    public async Task<Post> GetPost(long id) {
        using HttpClient client = new HttpClient(_handler);
        var response = await client.GetAsync(BasePath+$@"/{id}");
        
        // handling error responses
        if (!response.IsSuccessStatusCode) {
            Console.WriteLine($@"Error while getting post with id {id}!");
            Console.WriteLine("Error code: "+response.StatusCode);
            Console.WriteLine("Error message: "+ await response.Content.ReadAsStringAsync());
            throw new Exception($@"Failed to get post with id {id}! Error: {await response.Content.ReadAsStringAsync()}");
        }

        // handling successful responses
        var responseJsonString = await response.Content.ReadAsStringAsync();
        var returnedPost = JsonSerializer.Deserialize<Post>(responseJsonString);

        if (returnedPost == null)
            throw new JsonException("Couldn't parse returned post!");

        return returnedPost;
    }
}