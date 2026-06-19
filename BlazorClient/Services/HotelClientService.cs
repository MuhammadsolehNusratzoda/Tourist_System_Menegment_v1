using BlazorClient.Models;
using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorClient.Services;

public class HotelClientService
{
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorage;
    private static readonly JsonSerializerOptions _opts = new() { PropertyNameCaseInsensitive = true };

    // Demo data for when API is not running
    private static readonly List<HotelDto> _demoHotels = new()
    {
        new HotelDto { Id = Guid.NewGuid(), Name = "Dushanbe Grand Hotel", Description = "A luxurious 5-star hotel in the heart of Dushanbe, offering world-class amenities and stunning views of the Hissar Mountains.", Location = "Dushanbe", PricePerNight = 120, Stars = 5, Rating = 4.8m, ImageUrl = "https://images.unsplash.com/photo-1566073771259-6a8506099945?w=800" },
        new HotelDto { Id = Guid.NewGuid(), Name = "Khujand Palace", Description = "Elegant hotel by the Syr Darya river, the perfect base for exploring northern Tajikistan.", Location = "Khujand", PricePerNight = 75, Stars = 4, Rating = 4.5m, ImageUrl = "https://images.unsplash.com/photo-1582719478250-c89cae4dc85b?w=800" },
        new HotelDto { Id = Guid.NewGuid(), Name = "Pamir Lodge", Description = "Rustic yet comfortable lodge nestled in the majestic Pamir mountains, perfect for adventurers.", Location = "Khorog", PricePerNight = 45, Stars = 3, Rating = 4.6m, ImageUrl = "https://images.unsplash.com/photo-1520250497591-112f2f40a3f4?w=800" },
        new HotelDto { Id = Guid.NewGuid(), Name = "Istaravshan Heritage Inn", Description = "Historic inn located in the ancient silk road city, with traditional Tajik architecture.", Location = "Istaravshan", PricePerNight = 55, Stars = 3, Rating = 4.3m, ImageUrl = "https://images.unsplash.com/photo-1551882547-ff40c63fe5fa?w=800" },
        new HotelDto { Id = Guid.NewGuid(), Name = "Iskanderkul Resort", Description = "Breathtaking mountain resort by the legendary Iskanderkul lake, surrounded by pristine nature.", Location = "Sughd", PricePerNight = 90, Stars = 4, Rating = 4.7m, ImageUrl = "https://images.unsplash.com/photo-1571896349842-33c89424de2d?w=800" },
        new HotelDto { Id = Guid.NewGuid(), Name = "Wakhan Valley Guesthouse", Description = "Simple but charming guesthouse in the remote Wakhan corridor with incredible views.", Location = "Wakhan", PricePerNight = 30, Stars = 2, Rating = 4.4m, ImageUrl = "https://images.unsplash.com/photo-1506905925346-21bda4d32df4?w=800" },
    };

    public HotelClientService(HttpClient http, ILocalStorageService localStorage)
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

    public async Task<PagedResult<HotelDto>> GetAllAsync(int page = 1, int pageSize = 6)
    {
        try
        {
            var response = await _http.GetFromJsonAsync<ApiResponse<PagedResult<HotelDto>>>($"api/v1/hotels?page={page}&pageSize={pageSize}", _opts);
            return response?.Data ?? new PagedResult<HotelDto>();
        }
        catch
        {
            var paged = _demoHotels.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return new PagedResult<HotelDto> { Items = paged, TotalCount = _demoHotels.Count, Page = page, PageSize = pageSize };
        }
    }

    public async Task<HotelDto?> GetByIdAsync(Guid id)
    {
        try
        {
            var response = await _http.GetFromJsonAsync<ApiResponse<HotelDto>>($"api/v1/hotels/{id}", _opts);
            return response?.Data;
        }
        catch { return _demoHotels.FirstOrDefault(h => h.Id == id); }
    }

    public async Task<bool> CreateAsync(CreateHotelDto dto)
    {
        await SetAuthHeader();
        try
        {
            var response = await _http.PostAsJsonAsync("api/v1/hotels", dto);
            return response.IsSuccessStatusCode;
        }
        catch { return false; }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        await SetAuthHeader();
        try
        {
            var response = await _http.DeleteAsync($"api/v1/hotels/{id}");
            return response.IsSuccessStatusCode;
        }
        catch { return false; }
    }
}
