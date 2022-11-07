using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DNP1_Server.Controllers.ApiClasses; 

public class ApiPost {
    [Required]
    [JsonPropertyName("authorCookie")]
    public long AuthorCookie { get; set; }
    
    [Required]
    [JsonPropertyName("title")]
    public string Title { get; set; }
    
    [Required]
    [JsonPropertyName("body")]
    public string Body { get; set; }
}