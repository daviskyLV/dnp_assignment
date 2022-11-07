using System.Text.Json.Serialization;

namespace DNP1_Server.Controllers.ApiClasses;

public class ApiLogin {
    [JsonPropertyName("cookie")]
    public long Cookie { get; set; }
    
    [JsonPropertyName("username")]
    public string Title { get; set; }
    
    [JsonPropertyName("password")]
    public string Password { get; set; }
}