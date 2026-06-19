using Microsoft.EntityFrameworkCore;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly AppDbContext _ctx;
    public RestaurantRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task<PagedResult<Restaurant>> GetAllAsync(int page, int pageSize)
    {
        var total = await _ctx.Restaurants.CountAsync();
        var items = await _ctx.Restaurants.OrderByDescending(r => r.Rating)
            .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PagedResult<Restaurant> { Items = items, TotalCount = total, Page = page, PageSize = pageSize };
    }

    public async Task<Restaurant?> GetByIdAsync(Guid id) =>
        await _ctx.Restaurants.FirstOrDefaultAsync(r => r.Id == id);

    public async Task<PagedResult<Restaurant>> GetByCityAsync(string city, int page, int pageSize)
    {
        var query = _ctx.Restaurants.Where(r => r.Location.Contains(city));
        var total = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PagedResult<Restaurant> { Items = items, TotalCount = total, Page = page, PageSize = pageSize };
    }

    public async Task<PagedResult<Restaurant>> GetByCuisineAsync(string cuisineType, int page, int pageSize)
    {
        var query = _ctx.Restaurants.Where(r => r.CoisineType.Contains(cuisineType));
        var total = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PagedResult<Restaurant> { Items = items, TotalCount = total, Page = page, PageSize = pageSize };
    }

    public async Task AddAsync(Restaurant restaurant) { await _ctx.Restaurants.AddAsync(restaurant); await _ctx.SaveChangesAsync(); }
    public async Task UpdateAsync(Restaurant restaurant) { _ctx.Restaurants.Update(restaurant); await _ctx.SaveChangesAsync(); }
    public async Task DeleteAsync(Guid id)
    {
        var r = await _ctx.Restaurants.FindAsync(id);
        if (r != null) { _ctx.Restaurants.Remove(r); await _ctx.SaveChangesAsync(); }
    }
}
