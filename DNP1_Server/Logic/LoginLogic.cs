using DNP1_Server.Exceptions;

namespace DNP1_Server.Logic;

public class LoginLogic : ILoginLogic {
    private readonly Dictionary<string, string> _authCookie; // key is username
    private readonly Dictionary<string, string> _authUsernames; // key is cookie corresponding to username

    public LoginLogic() {
        _authCookie = new Dictionary<string, string>();
        _authUsernames = new Dictionary<string, string>();
    }

    public async Task<string> Login(string username, string password) {
        var response = await Program.Database.GetUserInfoAsync(username);

        if (response.Password != password)
            throw new DataMismatchException("Incorrect password!");

        if (_authCookie.ContainsKey(username))
            return _authCookie[username];
        
        // generating new cookie
        var rand = new Random();
        string cookie = rand.NextInt64()+"";
        while (_authUsernames.ContainsKey(cookie)) {
            cookie = rand.NextInt64()+"";
        }

        _authCookie.Add(username, cookie);
        _authUsernames.Add(cookie, username);
        return ""+cookie;
    }

    public async Task Logout(string username, string password) {
        var response = await Program.Database.GetUserInfoAsync(username);
        
        if (response.Password != password)
            throw new DataMismatchException("Incorrect password!");
        
        var cookie = _authCookie[username];
        _authCookie.Remove(username);
        _authUsernames.Remove(cookie);
    }

    

    public string UsernameFromCookie(string cookie) {
        if (!_authUsernames.ContainsKey(cookie))
            throw new NotFoundException("Cookie not found!");

        return _authUsernames[cookie];
    }
}