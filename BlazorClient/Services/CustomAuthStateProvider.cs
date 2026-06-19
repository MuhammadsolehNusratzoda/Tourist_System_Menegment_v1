using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;

namespace BlazorClient.Services;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationState _anonymous = new(new ClaimsPrincipal(new ClaimsIdentity()));

    public CustomAuthStateProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var token = await _localStorage.GetItemAsStringAsync("authToken");
            if (string.IsNullOrWhiteSpace(token))
                return _anonymous;

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            if (jwt.ValidTo < DateTime.UtcNow)
            {
                await _localStorage.RemoveItemAsync("authToken");
                return _anonymous;
            }

            var identity = new ClaimsIdentity(jwt.Claims, "jwt");
            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }
        catch
        {
            return _anonymous;
        }
    }

    public async Task NotifyUserAuthenticated(string token)
    {
        await _localStorage.SetItemAsStringAsync("authToken", token);
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);
        var identity = new ClaimsIdentity(jwt.Claims, "jwt");
        var user = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public async Task NotifyUserLoggedOut()
    {
        await _localStorage.RemoveItemAsync("authToken");
        await _localStorage.RemoveItemAsync("authUser");
        NotifyAuthenticationStateChanged(Task.FromResult(_anonymous));
    }
}
