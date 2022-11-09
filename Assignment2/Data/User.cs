namespace Assignment2.Data; 

public class User {
    public string Username { get; }
    public string Password { get; set; }

    public User(string username, string password) {
        Username = username;
        Password = password;
    }
}