public interface IHotelRepository
{
    Task<PagedResult<Hotel>> GetAllAsync(
        string? location, decimal? minPrice, decimal? maxPrice,
        int? minStars, int page, int pageSize);
    Task<Hotel?> GetByIdAsync(Guid id);
    Task AddAsync(Hotel hotel);
    Task UpdateAsync(Hotel hotel);
    Task DeleteAsync(Guid id);
}