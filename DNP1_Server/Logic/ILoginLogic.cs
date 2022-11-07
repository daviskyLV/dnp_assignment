namespace DNP1_Server.Logic; 

public interface ILoginLogic {
    /// <summary>
    /// Login a user into the system
    /// </summary>
    /// <param name="username">The username of the user</param>
    /// <param name="password">The password of the user</param>
    /// <returns>Login cookie to use for other operations</returns>
    long Login(string username, string password);

    /// <summary>
    /// Log out using username and password
    /// </summary>
    /// <param name="username">The user's username</param>
    /// <param name="password">The user's password</param>
    void Logout(string username, string password);

    /// <summary>
    /// Log out using cookie
    /// </summary>
    /// <param name="cookie">The cookie of the logged in user</param>
    void Logout(long cookie);

    /// <summary>
    /// Get username from a user's cookie
    /// </summary>
    /// <param name="cookie">The login cookie of the user</param>
    /// <returns>The username</returns>
    string UsernameFromCookie(long cookie);
}