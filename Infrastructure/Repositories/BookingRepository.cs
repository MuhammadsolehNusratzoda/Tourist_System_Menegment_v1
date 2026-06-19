using Microsoft.EntityFrameworkCore;

public class BookingRepository : IBookingRepository
{
    private readonly AppDbContext _ctx;
    public BookingRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task<PagedResult<Booking>> GetByUserIdAsync(Guid userId, int page, int pageSize)
    {
        var query = _ctx.Bookings.Where(b => b.UserId == userId).OrderByDescending(b => b.CreatedAt);
        var total = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PagedResult<Booking> { Items = items, TotalCount = total, Page = page, PageSize = pageSize };
    }

    public async Task<PagedResult<Booking>> GetAllAsync(int page, int pageSize)
    {
        var query = _ctx.Bookings.OrderByDescending(b => b.CreatedAt);
        var total = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PagedResult<Booking> { Items = items, TotalCount = total, Page = page, PageSize = pageSize };
    }

    public async Task<Booking?> GetByIdAsync(Guid id) =>
        await _ctx.Bookings.FirstOrDefaultAsync(b => b.Id == id);

    public async Task AddAsync(Booking booking) { await _ctx.Bookings.AddAsync(booking); await _ctx.SaveChangesAsync(); }
    public async Task UpdateAsync(Booking booking) { _ctx.Bookings.Update(booking); await _ctx.SaveChangesAsync(); }

    public async Task<List<Booking>> GetExpiredPendingAsync() =>
        await _ctx.Bookings
            .Where(b => b.Status == "Pending" && b.EndDate < DateTime.UtcNow)
            .ToListAsync();
}
