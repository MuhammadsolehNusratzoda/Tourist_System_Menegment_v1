using BlazorClient.Models;
using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorClient.Services;

public class BookingClientService
{
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorage;
    private static readonly JsonSerializerOptions _opts = new() { PropertyNameCaseInsensitive = true };

    // In-memory demo bookings
    private static readonly List<BookingDto> _demoBookings = new()
    {
        new BookingDto { Id = Guid.NewGuid(), BookingType = "Hotel", ReferenceId = Guid.NewGuid(), StartDate = DateTime.Today.AddDays(5), EndDate = DateTime.Today.AddDays(8), TotalPrice = 360, Status = "Confirmed", Notes = "Early check-in requested", CreatedAt = DateTime.Now.AddDays(-2) },
        new BookingDto { Id = Guid.NewGuid(), BookingType = "Guide", ReferenceId = Guid.NewGuid(), StartDate = DateTime.Today.AddDays(10), EndDate = DateTime.Today.AddDays(12), TotalPrice = 120, Status = "Pending", CreatedAt = DateTime.Now.AddDays(-1) },
        new BookingDto { Id = Guid.NewGuid(), BookingType = "Transport", ReferenceId = Guid.NewGuid(), StartDate = DateTime.Today.AddDays(-3), EndDate = DateTime.Today.AddDays(-3), TotalPrice = 35, Status = "Expired", CreatedAt = DateTime.Now.AddDays(-10) },
    };

    public BookingClientService(HttpClient http, ILocalStorageService localStorage)
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

    public async Task<PagedResult<BookingDto>> GetMyBookingsAsync(int page = 1, int pageSize = 10)
    {
        await SetAuthHeader();
        try
        {
            var response = await _http.GetFromJsonAsync<ApiResponse<PagedResult<BookingDto>>>($"api/v1/bookings/my?page={page}&pageSize={pageSize}", _opts);
            return response?.Data ?? new PagedResult<BookingDto>();
        }
        catch
        {
            var paged = _demoBookings.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return new PagedResult<BookingDto> { Items = paged, TotalCount = _demoBookings.Count, Page = page, PageSize = pageSize };
        }
    }

    public async Task<(bool success, string message)> CreateAsync(CreateBookingDto dto)
    {
        await SetAuthHeader();
        try
        {
            var response = await _http.PostAsJsonAsync("api/v1/bookings", dto);
            if (response.IsSuccessStatusCode)
                return (true, "Booking created successfully!");
            return (false, "Failed to create booking.");
        }
        catch
        {
            // Demo mode: add to local list
            _demoBookings.Add(new BookingDto
            {
                Id = Guid.NewGuid(),
                BookingType = dto.BookingType,
                ReferenceId = dto.ReferenceId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Status = "Pending",
                Notes = dto.Notes,
                CreatedAt = DateTime.Now
            });
            return (true, "Booking saved (demo mode)!");
        }
    }

    public async Task<(bool success, string message)> CancelAsync(Guid id)
    {
        await SetAuthHeader();
        try
        {
            var response = await _http.PatchAsync($"api/v1/bookings/{id}/cancel", null);
            if (response.IsSuccessStatusCode)
                return (true, "Booking cancelled.");
            return (false, "Cannot cancel this booking.");
        }
        catch
        {
            var booking = _demoBookings.FirstOrDefault(b => b.Id == id);
            if (booking != null) booking.Status = "Cancelled";
            return (true, "Booking cancelled (demo mode).");
        }
    }
}
