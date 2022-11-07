using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DNP1_Server.Controllers.ApiClasses;

public class ApiUser
{
    [Required]
    [JsonPropertyName("username")]
    public string UserName { get; set; }
    
    [Required]
    [JsonPropertyName("password")]
    public string Password { get; set; }
}