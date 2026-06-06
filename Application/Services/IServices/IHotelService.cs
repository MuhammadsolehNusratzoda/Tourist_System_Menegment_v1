public interface IHotelService
{
    Task<ApiResponse<PagedResult<HotelDto>>> GetAllAsync(int page, int pageSize);
    Task<ApiResponse<HotelDto>> GetByIdAsync(Guid id);
    Task<ApiResponse<PagedResult<HotelDto>>> GetByCityAsync(string city, int page, int pageSize);
    Task<ApiResponse<PagedResult<HotelDto>>> GetByStarsAsync(int stars, int page, int pageSize);
    Task<ApiResponse<List<HotelDto>>> GetTopRatedAsync(int count);
    Task<ApiResponse<HotelDto>> CreateAsync(CreateHotelDto dto);
    Task<ApiResponse<HotelDto>> UpdateAsync(Guid id, UpdateHotelDto dto);
    Task<ApiResponse<bool>> DeleteAsync(Guid id);
}