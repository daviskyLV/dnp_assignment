using System.Net.Http.Json;
using System.Text.Json;
using DNP1_Server.Controllers.ApiClasses;
using DNP1_Server.Utils;
using Microsoft.AspNetCore.Mvc;
using HttpCllients.ClientInterfaces;
namespace HttpCllients.Implementations;

public class UserHttpClient : IUserService
{
    private readonly HttpClient client;
    

    public UserHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<ActionResult<User>> CreateUser([FromBody] ApiUser dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/users", dto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        User user = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return user;
    }
}