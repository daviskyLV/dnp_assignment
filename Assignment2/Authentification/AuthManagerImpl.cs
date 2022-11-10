using System.Security.Claims;
using System.Text.Json;
using Assignment2.Data;
using Assignment2.Data.ClientInterfaces;
using Microsoft.JSInterop;

namespace Assignment2.Authentification;

public class AuthManagerImpl : IAuthManager {
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!;
    private readonly IUsersService _usersService;
    private readonly IJSRuntime _jsRuntime;

    public AuthManagerImpl(IUsersService usersService, IJSRuntime jsRuntime) {
        _usersService = usersService;
        _jsRuntime = jsRuntime;
    }

    public async Task LoginAsync(string username, string password) {
        string loginCookie = await _usersService.Login(new User(username, password)); // logging in via HTTP request
        
        var savedCookies = await GetCookieDataFromCacheAsync(); // getting saved cookies
        if (savedCookies == null)
            savedCookies = new CookieData{LoginCookie = loginCookie};

        savedCookies.LoginCookie = loginCookie; // updating login cookie with the new cookie
        await CacheCookieDataAsync(savedCookies);
        
        ClaimsPrincipal principal = CreateClaimsPrincipal(savedCookies); // convert saved cookies object to ClaimsPrincipal
        OnAuthStateChanged?.Invoke(principal); // notify interested classes in the change of authentication state
    }
    
    public async Task LogoutAsync(string username, string password) {
        string loginCookie = await _usersService.Logout(new User(username, password)); // logging out via HTTP request
        
        var savedCookies = await GetCookieDataFromCacheAsync(); // getting saved cookies
        if (savedCookies == null)
            savedCookies = new CookieData{LoginCookie = ""};

        savedCookies.LoginCookie = ""; // updating login cookie with the empty one
        await CacheCookieDataAsync(savedCookies);
        
        ClaimsPrincipal principal = CreateClaimsPrincipal(savedCookies); // convert saved cookies object to ClaimsPrincipal
        OnAuthStateChanged?.Invoke(principal); // notify interested classes in the change of authentication state
        //await ClearCookieDataFromCacheAsync(); // remove the cookie data object from browser cache
        //ClaimsPrincipal principal = CreateClaimsPrincipal(null); // create a new ClaimsPrincipal with nothing.
        //OnAuthStateChanged?.Invoke(principal); // notify about change in authentication state
    }

    public async Task CacheCookieDataAsync(CookieData cookieData) {
        string serialisedData = JsonSerializer.Serialize(cookieData);
        await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "cookieData", serialisedData);
    }
    
    private async Task ClearCookieDataFromCacheAsync() {
        await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "cookieData", "");
    }
    
    private static ClaimsIdentity ConvertCookieDataToClaimsIdentity(CookieData cookieData) {
        // here we take the information of the CookieData object and convert to Claims
        // this is (probably) the only method, which needs modifying for your own user type
        List<Claim> claims = new() {
            new Claim("login", cookieData.LoginCookie)
        };

        return new ClaimsIdentity(claims, "apiauth_type");
    }
    
    private static ClaimsPrincipal CreateClaimsPrincipal(CookieData? cookieData) {
        if (cookieData != null) {
            ClaimsIdentity identity = ConvertCookieDataToClaimsIdentity(cookieData);
            return new ClaimsPrincipal(identity);
        }

        return new ClaimsPrincipal();
    }
    
    private async Task<CookieData?> GetCookieDataFromCacheAsync() {
        string cookiesAsJson = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "cookieData");
        if (string.IsNullOrEmpty(cookiesAsJson)) return null;
        CookieData user = JsonSerializer.Deserialize<CookieData>(cookiesAsJson)!;
        return user;
    }
    
    public async Task<ClaimsPrincipal> GetAuthAsync() {
        // this method is called by the authentication framework, whenever user credentials are reguired
        CookieData? cookies =  await GetCookieDataFromCacheAsync(); // retrieve cached cookies, if any
        ClaimsPrincipal principal = CreateClaimsPrincipal(cookies); // create ClaimsPrincipal

        return principal;
    }
}