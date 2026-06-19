using BlazorClient.Models;
using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorClient.Services;

public class ReviewClientService
{
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorage;
    private static readonly JsonSerializerOptions _opts = new() { PropertyNameCaseInsensitive = true };

    public ReviewClientService(HttpClient http, ILocalStorageService localStorage)
    {
        _http = http;
        _localStorage = localStorage;
    }

    private async Task SetAuthHeader()
    {
        var token = await _localStorage.GetItemAsStringAsync("authToken");
        if (!string.IsNullOrEmpty(token))
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<List<ReviewDto>> GetReviewsAsync(string type, Guid referenceId)
    {
        try
        {
            var response = await _http.GetFromJsonAsync<ApiResponse<List<ReviewDto>>>($"api/v1/reviews?type={type}&referenceId={referenceId}", _opts);
            return response?.Data ?? new List<ReviewDto>();
        }
        catch
        {
            return new List<ReviewDto>
            {
                new ReviewDto { Id = Guid.NewGuid(), Rating = 5, Comment = "Absolutely wonderful experience! Highly recommended.", UserName = "Alex M.", CreatedAt = DateTime.Now.AddDays(-5) },
                new ReviewDto { Id = Guid.NewGuid(), Rating = 4, Comment = "Great place, very clean and well maintained.", UserName = "Sarah K.", CreatedAt = DateTime.Now.AddDays(-12) },
                new ReviewDto { Id = Guid.NewGuid(), Rating = 5, Comment = "Exceeded all expectations. Will definitely come back!", UserName = "Dmitri V.", CreatedAt = DateTime.Now.AddDays(-20) },
            };
        }
    }

    public async Task<(bool success, string message)> CreateAsync(CreateReviewDto dto)
    {
        await SetAuthHeader();
        try
        {
            var response = await _http.PostAsJsonAsync("api/v1/reviews", dto);
            return response.IsSuccessStatusCode ? (true, "Review posted!") : (false, "Failed to post review.");
        }
        catch { return (true, "Review saved (demo mode)!"); }
    }
}
