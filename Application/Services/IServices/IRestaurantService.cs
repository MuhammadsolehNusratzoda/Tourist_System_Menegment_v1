public interface IRestaurantService
{
    Task<ApiResponse<PagedResult<RestaurantDto>>> GetAllAsync(int page, int pageSize);
    Task<ApiResponse<RestaurantDto>> GetByIdAsync(Guid id);
    Task<ApiResponse<PagedResult<RestaurantDto>>> GetByCityAsync(string city, int page, int pageSize);
    Task<ApiResponse<PagedResult<RestaurantDto>>> GetByCuisineAsync(string cuisineType, int page, int pageSize);
    Task<ApiResponse<RestaurantDto>> CreateAsync(CreateRestaurantDto dto);
    Task<ApiResponse<RestaurantDto>> UpdateAsync(Guid id, UpdateRestaurantDto dto);
    Task<ApiResponse<bool>> DeleteAsync(Guid id);
}