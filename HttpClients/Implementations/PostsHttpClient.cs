using System.Net.Http.Json;
using System.Text.Json;
using DNP1_Server.Controllers.ApiClasses;
using DNP1_Server.Utils;
using HttpCllients.ClientInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace HttpCllients.Implementations;

public class PostsHttpClient : IPostsService
{
    private readonly HttpClient client;


    public PostsHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<ActionResult<Post>> CreatePost([FromBody] ApiPost dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/users", dto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        Post post = JsonSerializer.Deserialize<Post>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return post;
    }
}