public interface IGuidRepository
{
    Task<PagedResult<Guide>> GetAllAsync(int page, int pageSize);
    Task<Guide?> GetByIdAsync(Guid id);
    Task AddAsync(Guide guide);
    Task UpdateAsync(Guide guide);
    Task DeleteAsync(Guid id);
    Task<PagedResult<Guide>> GetByLanguageAsync(string language, int page, int pageSize);
    Task<List<Guide>> GetTopRatedAsync(int count); // For Background Service / Caching
}