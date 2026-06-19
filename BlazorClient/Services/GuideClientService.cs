using BlazorClient.Models;
using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorClient.Services;

public class GuideClientService
{
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorage;
    private static readonly JsonSerializerOptions _opts = new() { PropertyNameCaseInsensitive = true };

    private static readonly List<GuideDto> _demoGuides = new()
    {
        new GuideDto { Id = Guid.NewGuid(), Name = "Ahmad Rahimov", Bio = "Professional mountain guide with 10 years of experience in the Pamirs. Speaks English, Russian and Tajik.", Languages = "Tajik, English, Russian", PricePerDay = 60, ExperienceYears = 10, Rating = 4.9m, ImageUrl = "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=400" },
        new GuideDto { Id = Guid.NewGuid(), Name = "Zulfiya Nazarova", Bio = "Cultural guide specializing in Dushanbe's historical sites and Tajik cuisine tours.", Languages = "Tajik, English, Persian", PricePerDay = 45, ExperienceYears = 6, Rating = 4.8m, ImageUrl = "https://images.unsplash.com/photo-1494790108377-be9c29b29330?w=400" },
        new GuideDto { Id = Guid.NewGuid(), Name = "Rustam Karimov", Bio = "Adventure trekking guide certified for high-altitude expeditions in the Fan Mountains.", Languages = "Tajik, Russian, German", PricePerDay = 70, ExperienceYears = 12, Rating = 4.7m, ImageUrl = "https://images.unsplash.com/photo-1500648767791-00dcc994a43e?w=400" },
        new GuideDto { Id = Guid.NewGuid(), Name = "Malika Tursunova", Bio = "Silk Road history expert offering guided tours through ancient caravanserai and bazaars.", Languages = "Tajik, English, French", PricePerDay = 50, ExperienceYears = 8, Rating = 4.6m, ImageUrl = "https://images.unsplash.com/photo-1438761681033-6461ffad8d80?w=400" },
        new GuideDto { Id = Guid.NewGuid(), Name = "Behruz Saidov", Bio = "Wildlife and bird-watching specialist with deep knowledge of Tajikistan's ecosystems.", Languages = "Tajik, English", PricePerDay = 55, ExperienceYears = 9, Rating = 4.5m, ImageUrl = "https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?w=400" },
    };

    public GuideClientService(HttpClient http, ILocalStorageService localStorage)
    {
        _http = http;
        _localStorage = localStorage;
    }

    public async Task<PagedResult<GuideDto>> GetAllAsync(int page = 1, int pageSize = 6)
    {
        try
        {
            var response = await _http.GetFromJsonAsync<ApiResponse<PagedResult<GuideDto>>>($"api/v1/guides?page={page}&pageSize={pageSize}", _opts);
            return response?.Data ?? new PagedResult<GuideDto>();
        }
        catch
        {
            var paged = _demoGuides.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return new PagedResult<GuideDto> { Items = paged, TotalCount = _demoGuides.Count, Page = page, PageSize = pageSize };
        }
    }

    public async Task<GuideDto?> GetByIdAsync(Guid id)
    {
        try
        {
            var response = await _http.GetFromJsonAsync<ApiResponse<GuideDto>>($"api/v1/guides/{id}", _opts);
            return response?.Data;
        }
        catch { return _demoGuides.FirstOrDefault(g => g.Id == id); }
    }
}
