using System.ComponentModel.DataAnnotations;

namespace DNP1_Server.Utils;

public class Post {
    [Key]
    [Required]
    public string Id { get; init; }
    [Required]
    public string Author { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Body { get; set; }
}    