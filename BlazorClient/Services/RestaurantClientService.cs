using BlazorClient.Models;
using Blazored.LocalStorage;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorClient.Services;

public class RestaurantClientService
{
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorage;
    private static readonly JsonSerializerOptions _opts = new() { PropertyNameCaseInsensitive = true };

    private static readonly List<RestaurantDto> _demoRestaurants = new()
    {
        new RestaurantDto { Id = Guid.NewGuid(), Name = "Chayhona Rohat", Description = "Traditional Tajik teahouse with an outdoor garden serving authentic Tajik cuisine.", Location = "Dushanbe", CuisineType = "Tajik", PriceRange = "$$", OpeningHours = "08:00-23:00", PhoneNumber = "+992 37 221-1234", Rating = 4.7m, ImageUrl = "https://images.unsplash.com/photo-1517248135467-4c7edcad34c4?w=800" },
        new RestaurantDto { Id = Guid.NewGuid(), Name = "Pamir Restaurant", Description = "Rooftop dining with panoramic views of the city. Specializes in Central Asian and Pamiri dishes.", Location = "Dushanbe", CuisineType = "Central Asian", PriceRange = "$$$", OpeningHours = "12:00-00:00", PhoneNumber = "+992 37 227-5678", Rating = 4.5m, ImageUrl = "https://images.unsplash.com/photo-1555396273-367ea4eb4db5?w=800" },
        new RestaurantDto { Id = Guid.NewGuid(), Name = "Silk Road Cafe", Description = "Cozy cafe on the ancient Silk Road offering fusion cuisine blending Tajik and Mediterranean flavors.", Location = "Khujand", CuisineType = "Fusion", PriceRange = "$$", OpeningHours = "09:00-22:00", PhoneNumber = "+992 34 226-9012", Rating = 4.4m, ImageUrl = "https://images.unsplash.com/photo-1414235077428-338989a2e8c0?w=800" },
        new RestaurantDto { Id = Guid.NewGuid(), Name = "Hissar Grill House", Description = "Authentic barbecue restaurant near the Hissar fortress, serving fresh locally-sourced meats.", Location = "Hissar", CuisineType = "BBQ", PriceRange = "$$", OpeningHours = "11:00-22:00", PhoneNumber = "+992 37 235-3456", Rating = 4.6m, ImageUrl = "https://images.unsplash.com/photo-1504674900247-0877df9cc836?w=800" },
        new RestaurantDto { Id = Guid.NewGuid(), Name = "Yak Restaurant", Description = "High-altitude dining experience in the Pamirs with warming traditional Pamiri dishes.", Location = "Khorog", CuisineType = "Pamiri", PriceRange = "$", OpeningHours = "07:00-21:00", PhoneNumber = "+992 35 222-7890", Rating = 4.8m, ImageUrl = "https://images.unsplash.com/photo-1466978913421-dad2ebd01d17?w=800" },
    };

    public RestaurantClientService(HttpClient http, ILocalStorageService localStorage)
    {
        _http = http;
        _localStorage = localStorage;
    }

    public async Task<PagedResult<RestaurantDto>> GetAllAsync(int page = 1, int pageSize = 6)
    {
        try
        {
            var response = await _http.GetFromJsonAsync<ApiResponse<PagedResult<RestaurantDto>>>($"api/v1/restaurants?page={page}&pageSize={pageSize}", _opts);
            return response?.Data ?? new PagedResult<RestaurantDto>();
        }
        catch
        {
            var paged = _demoRestaurants.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return new PagedResult<RestaurantDto> { Items = paged, TotalCount = _demoRestaurants.Count, Page = page, PageSize = pageSize };
        }
    }
}
