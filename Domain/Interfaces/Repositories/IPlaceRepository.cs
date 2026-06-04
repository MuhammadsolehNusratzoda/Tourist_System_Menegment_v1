public interface IPlaceRepository
{
    Task<PagedResult<Place>> GetAllAsync(string? location, int page, int pageSize);
    Task<Place?> GetByIdAsync(Guid id);
    Task AddAsync(Place place);
    Task UpdateAsync(Place place);
    Task DeleteAsync(Guid id);
}