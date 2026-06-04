public interface IPlaceService
{
    Task<ApiResponse<PagedResult<PlaceDto>>> GetAllAsync(string? location, int page, int pageSize);
    Task<ApiResponse<PlaceDto>> GetByIdAsync(Guid id);
    Task<ApiResponse<PlaceDto>> CreateAsync(CreatePlaceDto dto, Guid userId);
    Task<ApiResponse<PlaceDto>> UpdateAsync(Guid id, UpdatePlaceDto dto, Guid userId, string role);
    Task<ApiResponse<bool>> DeleteAsync(Guid id, Guid userId, string role);
}