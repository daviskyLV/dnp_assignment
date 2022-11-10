namespace Assignment2.Data.ClientInterfaces; 

public interface IUsersService {
    public Task<string> Login(User user);
    public Task<string> Logout(User user);
    public Task<User> Register(User user);
}