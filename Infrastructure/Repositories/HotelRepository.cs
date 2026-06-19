using Microsoft.EntityFrameworkCore;

public class HotelRepository : IHotelRepository
{
    private readonly AppDbContext _ctx;
    public HotelRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task<PagedResult<Hotel>> GetAllAsync(
        string? location, decimal? minPrice, decimal? maxPrice, int? minStars, int page, int pageSize)
    {
        var query = _ctx.Hotels.AsQueryable();
        if (!string.IsNullOrEmpty(location))
            query = query.Where(h => h.Location.Contains(location));
        if (minPrice.HasValue) query = query.Where(h => h.PricePerNight >= minPrice.Value);
        if (maxPrice.HasValue) query = query.Where(h => h.PricePerNight <= maxPrice.Value);
        if (minStars.HasValue) query = query.Where(h => h.Stars >= minStars.Value);

        var total = await query.CountAsync();
        var items = await query.OrderByDescending(h => h.Rating)
            .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PagedResult<Hotel> { Items = items, TotalCount = total, Page = page, PageSize = pageSize };
    }

    public async Task<Hotel?> GetByIdAsync(Guid id) =>
        await _ctx.Hotels.FirstOrDefaultAsync(h => h.Id == id);

    public async Task AddAsync(Hotel hotel) { await _ctx.Hotels.AddAsync(hotel); await _ctx.SaveChangesAsync(); }
    public async Task UpdateAsync(Hotel hotel) { _ctx.Hotels.Update(hotel); await _ctx.SaveChangesAsync(); }
    public async Task DeleteAsync(Guid id)
    {
        var h = await _ctx.Hotels.FindAsync(id);
        if (h != null) { _ctx.Hotels.Remove(h); await _ctx.SaveChangesAsync(); }
    }
}
