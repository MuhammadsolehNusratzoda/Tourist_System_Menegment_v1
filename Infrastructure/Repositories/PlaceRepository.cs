using Microsoft.EntityFrameworkCore;

public class PlaceRepository : IPlaceRepository
{
    private readonly AppDbContext _ctx;
    public PlaceRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task<PagedResult<Place>> GetAllAsync(string? location, int page, int pageSize)
    {
        var query = _ctx.Places.AsQueryable();
        if (!string.IsNullOrEmpty(location))
            query = query.Where(p => p.Location.Contains(location));

        var total = await query.CountAsync();
        var items = await query
            .OrderByDescending(p => (double)p.Rating)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Place> { Items = items, TotalCount = total, Page = page, PageSize = pageSize };
    }

    public async Task<Place?> GetByIdAsync(Guid id) =>
        await _ctx.Places.FirstOrDefaultAsync(p => p.Id == id);

    public async Task AddAsync(Place place) { await _ctx.Places.AddAsync(place); await _ctx.SaveChangesAsync(); }
    public async Task UpdateAsync(Place place) { _ctx.Places.Update(place); await _ctx.SaveChangesAsync(); }
    public async Task DeleteAsync(Guid id)
    {
        var p = await _ctx.Places.FindAsync(id);
        if (p != null) { _ctx.Places.Remove(p); await _ctx.SaveChangesAsync(); }
    }
}
