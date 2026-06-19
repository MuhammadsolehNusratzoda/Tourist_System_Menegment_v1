using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/v1/admin")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly AppDbContext _context;

    public AdminController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("stats")]
    public async Task<IActionResult> GetStats()
    {
        var totalBookings = await _context.Bookings.CountAsync();
        var totalHotels = await _context.Hotels.CountAsync();
        var totalGuides = await _context.Guides.CountAsync();
        var totalPlaces = await _context.Places.CountAsync();
        var totalRestaurants = await _context.Restaurants.CountAsync();
        var totalUsers = await _context.Users.CountAsync();
        var totalRevenue = await _context.Bookings
            .Where(b => b.Status == "Confirmed")
            .SumAsync(b => b.TotalPrice);

        return Ok(ApiResponse<object>.Ok(new
        {
            TotalBookings = totalBookings,
            TotalHotels = totalHotels,
            TotalGuides = totalGuides,
            TotalPlaces = totalPlaces,
            TotalRestaurants = totalRestaurants,
            TotalUsers = totalUsers,
            TotalRevenue = totalRevenue
        }));
    }
}
