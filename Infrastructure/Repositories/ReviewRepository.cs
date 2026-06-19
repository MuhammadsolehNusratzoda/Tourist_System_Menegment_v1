using Microsoft.EntityFrameworkCore;

public class ReviewRepository : IReviewRepository
{
    private readonly AppDbContext _ctx;
    public ReviewRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task<List<Review>> GetByReferenceAsync(string reviewType, Guid referenceId) =>
        await _ctx.Reviews
            .Where(r => r.ReviewType == reviewType && r.ReferenceId == referenceId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();

    public async Task AddAsync(Review review) { await _ctx.Reviews.AddAsync(review); await _ctx.SaveChangesAsync(); }

    public async Task DeleteAsync(Guid id)
    {
        var r = await _ctx.Reviews.FindAsync(id);
        if (r != null) { _ctx.Reviews.Remove(r); await _ctx.SaveChangesAsync(); }
    }
}
