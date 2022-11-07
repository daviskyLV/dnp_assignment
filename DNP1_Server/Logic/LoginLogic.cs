using DNP1_Server.Database.Enums;

namespace DNP1_Server.Logic;

public class LoginLogic : ILoginLogic {
    private readonly Dictionary<string, long> _authCookie; // key is username
    private readonly Dictionary<long, string> _authUsernames; // key is cookie corresponding to username

    public LoginLogic() {
        _authCookie = new Dictionary<string, long>();
        _authUsernames = new Dictionary<long, string>();
    }

    public long Login(string username, string password) {
        var dbResponse = Program.Database.GetUserInfo(username);

        switch (dbResponse.Item1) {
            case GetUserEnum.NotFound: {
                throw new Exception("Username not found");
            }
            case GetUserEnum.Success: {
                if (dbResponse.Item2.Password == password) {
                    if (!_authCookie.ContainsKey(username)) {
                        var rand = new Random();
                        long cookie = rand.NextInt64();
                        while (_authCookie.ContainsValue(cookie)) {
                            cookie = rand.NextInt64();
                        }

                        _authCookie.Add(username, cookie);
                        _authUsernames.Add(cookie, username);
                        return cookie;
                    }

                    return _authCookie[username];
                }

                throw new Exception("Incorrect password");
            }
            default: throw new Exception("Internal error");
        }
    }

    public void Logout(string username, string password) {
        var dbResponse = Program.Database.GetUserInfo(username);

        switch (dbResponse.Item1) {
            case GetUserEnum.NotFound: {
                throw new Exception("Username not found");
            }
            case GetUserEnum.Success: {
                if (dbResponse.Item2.Password == password) {
                    var cookie = _authCookie[username];
                    _authCookie.Remove(username);
                    _authUsernames.Remove(cookie);
                }

                throw new Exception("Incorrect password");
            }
            default: throw new Exception("Internal error");
        }
    }

    public void Logout(long cookie) {
        if (!_authCookie.ContainsValue(cookie))
            throw new Exception("Cookie not found");

        var username = _authUsernames[cookie];
        _authUsernames.Remove(cookie);
        _authCookie.Remove(username);
    }

    public string UsernameFromCookie(long cookie) {
        if (!_authCookie.ContainsValue(cookie))
            throw new Exception("Cookie not found");

        return _authUsernames[cookie];
    }
}