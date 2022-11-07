using System.Text.Json.Serialization;

namespace DNP1_Server.Controllers.ApiClasses;

public class ApiLogin {

    [JsonPropertyName("username")]
    public string Username { get; set; }
    
    [JsonPropertyName("password")]
    public string Password { get; set; }
}