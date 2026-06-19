using BlazorClient.Models;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorClient.Services;

public class ClientAuthService
{
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorage;
    private readonly CustomAuthStateProvider _authProvider;

    public ClientAuthService(HttpClient http, ILocalStorageService localStorage,
        AuthenticationStateProvider authProvider)
    {
        _http = http;
        _localStorage = localStorage;
        _authProvider = (CustomAuthStateProvider)authProvider;
    }

    public async Task<(bool success, string message)> LoginAsync(LoginRequest request)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/v1/auth/login", request);
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<AuthResponse>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (result?.Success == true && result.Data != null)
            {
                await _localStorage.SetItemAsStringAsync("authToken", result.Data.Token);
                await _localStorage.SetItemAsStringAsync("authUser", JsonSerializer.Serialize(result.Data));
                await _authProvider.NotifyUserAuthenticated(result.Data.Token);
                return (true, "Login successful!");
            }
            return (false, result?.Message ?? "Login failed.");
        }
        catch
        {
            // Demo mode: create a mock token for UI testing
            var mockUser = new AuthResponse { FullName = request.Email.Split('@')[0], Email = request.Email, Role = "Tourist", ExpiresAt = DateTime.UtcNow.AddHours(24) };
            await _localStorage.SetItemAsStringAsync("authUser", JsonSerializer.Serialize(mockUser));
            return (false, "API not reachable. Running in demo mode.");
        }
    }

    public async Task<(bool success, string message)> RegisterAsync(RegisterRequest request)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/v1/auth/register", request);
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<AuthResponse>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (result?.Success == true && result.Data != null)
            {
                await _localStorage.SetItemAsStringAsync("authToken", result.Data.Token);
                await _authProvider.NotifyUserAuthenticated(result.Data.Token);
                return (true, "Registration successful!");
            }
            return (false, result?.Message ?? "Registration failed.");
        }
        catch
        {
            return (false, "API not reachable. Please try again later.");
        }
    }

    public async Task LogoutAsync()
    {
        await _authProvider.NotifyUserLoggedOut();
    }

    public async Task<AuthResponse?> GetCurrentUserAsync()
    {
        try
        {
            var json = await _localStorage.GetItemAsStringAsync("authUser");
            if (string.IsNullOrEmpty(json)) return null;
            return JsonSerializer.Deserialize<AuthResponse>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch { return null; }
    }
}
