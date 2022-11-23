using System.ComponentModel.DataAnnotations;

namespace DNP1_Server.Utils; 

/// <summary>
/// Contains information about a user
/// </summary>
public class User {
    [Key]
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }

    public User(string username, string password) {
        Username = username;
        Password = password;
    }
    
    private User() { }
}