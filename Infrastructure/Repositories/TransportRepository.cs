using Microsoft.EntityFrameworkCore;

public class TransportRepository : ITransportRepository
{
    private readonly AppDbContext _ctx;
    public TransportRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task<PagedResult<Transport>> GetAllAsync(int page, int pageSize)
    {
        var total = await _ctx.Transports.CountAsync();
        var items = await _ctx.Transports.OrderBy(t => t.DepartureTime)
            .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PagedResult<Transport> { Items = items, TotalCount = total, Page = page, PageSize = pageSize };
    }

    public async Task<Transport?> GetByIdAsync(Guid id) =>
        await _ctx.Transports.FirstOrDefaultAsync(t => t.Id == id);

    public async Task<PagedResult<Transport>> GetByTypeAsync(string transportType, int page, int pageSize)
    {
        var query = _ctx.Transports.Where(t => t.Type == transportType);
        var total = await query.CountAsync();
        var items = await query.OrderBy(t => t.DepartureTime).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PagedResult<Transport> { Items = items, TotalCount = total, Page = page, PageSize = pageSize };
    }

    public async Task<List<Transport>> GetAvailableAsync(DateTime date) =>
        await _ctx.Transports
            .Where(t => t.DepartureTime.Date == date.Date && t.AvailableSeats > 0)
            .ToListAsync();

    public async Task AddAsync(Transport transport) { await _ctx.Transports.AddAsync(transport); await _ctx.SaveChangesAsync(); }
    public async Task UpdateAsync(Transport transport) { _ctx.Transports.Update(transport); await _ctx.SaveChangesAsync(); }
    public async Task DeleteAsync(Guid id)
    {
        var t = await _ctx.Transports.FindAsync(id);
        if (t != null) { _ctx.Transports.Remove(t); await _ctx.SaveChangesAsync(); }
    }
}
