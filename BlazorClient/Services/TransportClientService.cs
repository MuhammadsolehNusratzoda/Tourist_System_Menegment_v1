using BlazorClient.Models;
using Blazored.LocalStorage;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorClient.Services;

public class TransportClientService
{
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorage;
    private static readonly JsonSerializerOptions _opts = new() { PropertyNameCaseInsensitive = true };

    private static readonly List<TransportDto> _demoTransports = new()
    {
        new TransportDto { Id = Guid.NewGuid(), Name = "Express Bus DU→KH", Type = "Bus", Origin = "Dushanbe", Destination = "Khujand", DepartureTime = DateTime.Today.AddHours(7), ArrivalTime = DateTime.Today.AddHours(14), PricePerSeat = 12, AvailableSeats = 28 },
        new TransportDto { Id = Guid.NewGuid(), Name = "Pamir Jeep Service", Type = "Car", Origin = "Dushanbe", Destination = "Khorog", DepartureTime = DateTime.Today.AddHours(6), ArrivalTime = DateTime.Today.AddHours(18), PricePerSeat = 35, AvailableSeats = 4 },
        new TransportDto { Id = Guid.NewGuid(), Name = "City Taxi DU→HI", Type = "Taxi", Origin = "Dushanbe", Destination = "Hissar", DepartureTime = DateTime.Today.AddHours(9), ArrivalTime = DateTime.Today.AddHours(10), PricePerSeat = 5, AvailableSeats = 3 },
        new TransportDto { Id = Guid.NewGuid(), Name = "Northern Train KH→IS", Type = "Train", Origin = "Khujand", Destination = "Istaravshan", DepartureTime = DateTime.Today.AddHours(8), ArrivalTime = DateTime.Today.AddHours(11), PricePerSeat = 8, AvailableSeats = 60 },
        new TransportDto { Id = Guid.NewGuid(), Name = "Fan Mountains Shuttle", Type = "Car", Origin = "Dushanbe", Destination = "Seven Lakes", DepartureTime = DateTime.Today.AddHours(6), ArrivalTime = DateTime.Today.AddHours(11).AddMinutes(30), PricePerSeat = 20, AvailableSeats = 8 },
        new TransportDto { Id = Guid.NewGuid(), Name = "Wakhan Corridor 4x4", Type = "Car", Origin = "Khorog", Destination = "Wakhan Valley", DepartureTime = DateTime.Today.AddHours(7), ArrivalTime = DateTime.Today.AddHours(13), PricePerSeat = 45, AvailableSeats = 4 },
    };

    public TransportClientService(HttpClient http, ILocalStorageService localStorage)
    {
        _http = http;
        _localStorage = localStorage;
    }

    public async Task<PagedResult<TransportDto>> GetAllAsync(string? type = null, int page = 1, int pageSize = 6)
    {
        try
        {
            var url = $"api/v1/transport?page={page}&pageSize={pageSize}";
            if (!string.IsNullOrEmpty(type)) url += $"&type={type}";
            var response = await _http.GetFromJsonAsync<ApiResponse<PagedResult<TransportDto>>>(url, _opts);
            return response?.Data ?? new PagedResult<TransportDto>();
        }
        catch
        {
            var filtered = string.IsNullOrEmpty(type) ? _demoTransports : _demoTransports.Where(t => t.Type == type).ToList();
            var paged = filtered.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return new PagedResult<TransportDto> { Items = paged, TotalCount = filtered.Count, Page = page, PageSize = pageSize };
        }
    }
}
