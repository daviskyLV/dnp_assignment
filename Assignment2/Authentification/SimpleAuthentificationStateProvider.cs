using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Assignment2.Authentification; 

public class SimpleAuthentificationStateProvider : AuthenticationStateProvider {
    private readonly IAuthManager _authManager;

    public SimpleAuthentificationStateProvider(IAuthManager authManager) {
        _authManager = authManager;
        authManager.OnAuthStateChanged += AuthStateChanged;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync() {
        ClaimsPrincipal principal = await _authManager.GetAuthAsync();
        return await Task.FromResult(new AuthenticationState(principal));
    }

    private void AuthStateChanged(ClaimsPrincipal principal) {
        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(principal)));
    }
}