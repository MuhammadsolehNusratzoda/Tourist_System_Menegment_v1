using BlazorClient.Models;
using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorClient.Services;

public class PlaceClientService
{
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorage;
    private static readonly JsonSerializerOptions _opts = new() { PropertyNameCaseInsensitive = true };

    private static readonly List<PlaceDto> _demoPlaces = new()
    {
        new PlaceDto { Id = Guid.NewGuid(), Name = "Iskanderkul Lake", Description = "A stunning turquoise mountain lake named after Alexander the Great, nestled at 2,200m altitude.", Location = "Sughd Region", EntryFee = 5, Rating = 4.9m, Latitude = 39.0844, Longitude = 68.3753, ImageUrl = "https://images.unsplash.com/photo-1506905925346-21bda4d32df4?w=800" },
        new PlaceDto { Id = Guid.NewGuid(), Name = "Hissar Fortress", Description = "Ancient fortress with over 3,000 years of history, once a strategic silk road gateway.", Location = "Hissar", EntryFee = 3, Rating = 4.6m, Latitude = 38.5314, Longitude = 68.5455, ImageUrl = "https://images.unsplash.com/photo-1552832230-c0197dd311b5?w=800" },
        new PlaceDto { Id = Guid.NewGuid(), Name = "Pamir Highway", Description = "One of the world's highest roads (M41), offering breathtaking views across the Roof of the World.", Location = "GBAO Region", EntryFee = 0, Rating = 4.8m, Latitude = 37.5000, Longitude = 73.0000, ImageUrl = "https://images.unsplash.com/photo-1519608487953-e999c86e7455?w=800" },
        new PlaceDto { Id = Guid.NewGuid(), Name = "Seven Lakes", Description = "A magical chain of 7 turquoise mountain lakes in the Fan Mountains, perfect for trekking.", Location = "Sughd", EntryFee = 2, Rating = 4.9m, Latitude = 39.2200, Longitude = 68.2800, ImageUrl = "https://images.unsplash.com/photo-1464822759023-fed622ff2c3b?w=800" },
        new PlaceDto { Id = Guid.NewGuid(), Name = "National Museum of Tajikistan", Description = "Learn about Tajikistan's rich history and culture, from ancient Aryan civilization to modern times.", Location = "Dushanbe", EntryFee = 4, Rating = 4.3m, Latitude = 38.5598, Longitude = 68.7738, ImageUrl = "https://images.unsplash.com/photo-1554907984-15263bfd63bd?w=800" },
        new PlaceDto { Id = Guid.NewGuid(), Name = "Wakhan Corridor", Description = "Remote valley bordering Afghanistan and China, with ancient petroglyphs and incredible mountain views.", Location = "GBAO", EntryFee = 0, Rating = 4.7m, Latitude = 37.2000, Longitude = 73.5000, ImageUrl = "https://images.unsplash.com/photo-1569949381669-ecf31ae8e613?w=800" },
    };

    public PlaceClientService(HttpClient http, ILocalStorageService localStorage)
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

    public async Task<PagedResult<PlaceDto>> GetAllAsync(string? location = null, int page = 1, int pageSize = 6)
    {
        try
        {
            var url = $"api/v1/places?page={page}&pageSize={pageSize}";
            if (!string.IsNullOrEmpty(location)) url += $"&location={location}";
            var response = await _http.GetFromJsonAsync<ApiResponse<PagedResult<PlaceDto>>>(url, _opts);
            return response?.Data ?? new PagedResult<PlaceDto>();
        }
        catch
        {
            var filtered = string.IsNullOrEmpty(location) ? _demoPlaces : _demoPlaces.Where(p => p.Location.Contains(location, StringComparison.OrdinalIgnoreCase)).ToList();
            var paged = filtered.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return new PagedResult<PlaceDto> { Items = paged, TotalCount = filtered.Count, Page = page, PageSize = pageSize };
        }
    }

    public async Task<PlaceDto?> GetByIdAsync(Guid id)
    {
        try
        {
            var response = await _http.GetFromJsonAsync<ApiResponse<PlaceDto>>($"api/v1/places/{id}", _opts);
            return response?.Data;
        }
        catch { return _demoPlaces.FirstOrDefault(p => p.Id == id); }
    }

    public async Task<bool> CreateAsync(CreatePlaceDto dto)
    {
        await SetAuthHeader();
        try
        {
            var response = await _http.PostAsJsonAsync("api/v1/places", dto);
            return response.IsSuccessStatusCode;
        }
        catch { return false; }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        await SetAuthHeader();
        try
        {
            var response = await _http.DeleteAsync($"api/v1/places/{id}");
            return response.IsSuccessStatusCode;
        }
        catch { return false; }
    }
}
