public interface IBookingService
{
    Task<ApiResponse<BookingDto>> CreateAsync(CreateBookingDto dto, Guid userId);
    Task<ApiResponse<bool>> CancelAsync(Guid id, Guid userId, string role);
    Task<ApiResponse<PagedResult<BookingDto>>> GetByUserIdAsync(Guid userId, int page, int pageSize);
    Task<ApiResponse<PagedResult<BookingDto>>> GetAllAsync(int page, int pageSize); // Admin only
    Task<ApiResponse<BookingDto>> GetByIdAsync(Guid id);
    Task<ApiResponse<bool>> UpdateStatusAsync(Guid id, string status);
}