namespace DNP1_Server.Utils; 

/// <summary>
/// Contains information about a user
/// </summary>
public class User {
    private readonly string _username;
    public string Password { get; set; }

    public User(string username) {
        _username = username;
    }

    public string GetUsername() {
        return _username;
    }
}