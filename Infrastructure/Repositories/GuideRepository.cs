using Microsoft.EntityFrameworkCore;

// Note: Interface is named IGuidRepository (typo in original), keeping as-is
public class GuideRepository : IGuidRepository
{
    private readonly AppDbContext _ctx;
    public GuideRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task<PagedResult<Guide>> GetAllAsync(int page, int pageSize)
    {
        var total = await _ctx.Guides.CountAsync();
        var items = await _ctx.Guides.OrderByDescending(g => g.Rating)
            .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PagedResult<Guide> { Items = items, TotalCount = total, Page = page, PageSize = pageSize };
    }

    public async Task<Guide?> GetByIdAsync(Guid id) =>
        await _ctx.Guides.FirstOrDefaultAsync(g => g.Id == id);

    public async Task<PagedResult<Guide>> GetByLanguageAsync(string language, int page, int pageSize)
    {
        var query = _ctx.Guides.Where(g => g.Languages.Contains(language));
        var total = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PagedResult<Guide> { Items = items, TotalCount = total, Page = page, PageSize = pageSize };
    }

    public async Task<List<Guide>> GetTopRatedAsync(int count) =>
        await _ctx.Guides.OrderByDescending(g => g.Rating).Take(count).ToListAsync();

    public async Task AddAsync(Guide guide) { await _ctx.Guides.AddAsync(guide); await _ctx.SaveChangesAsync(); }
    public async Task UpdateAsync(Guide guide) { _ctx.Guides.Update(guide); await _ctx.SaveChangesAsync(); }
    public async Task DeleteAsync(Guid id)
    {
        var g = await _ctx.Guides.FindAsync(id);
        if (g != null) { _ctx.Guides.Remove(g); await _ctx.SaveChangesAsync(); }
    }
}
