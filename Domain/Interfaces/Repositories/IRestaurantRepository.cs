public interface IRestaurantRepository
{
    Task<PagedResult<Restaurant>> GetAllAsync(int page, int pageSize);
    Task<Restaurant?> GetByIdAsync(Guid id);
    Task AddAsync(Restaurant restaurant);
    Task UpdateAsync(Restaurant restaurant);
    Task DeleteAsync(Guid id);
    Task<PagedResult<Restaurant>> GetByCityAsync(string city, int page, int pageSize);
    Task<PagedResult<Restaurant>> GetByCuisineAsync(string cuisineType, int page, int pageSize);
}