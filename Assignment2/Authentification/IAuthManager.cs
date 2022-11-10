using System.Security.Claims;

namespace Assignment2.Authentification; 

public interface IAuthManager {
    public Task LoginAsync(string username, string password);
    public Task LogoutAsync(string username, string password);
    public Task<ClaimsPrincipal> GetAuthAsync();

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
}