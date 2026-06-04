public interface IBookingRepository
{
    Task<PagedResult<Booking>> GetByUserIdAsync(Guid userId, int page, int pageSize);
    Task<PagedResult<Booking>> GetAllAsync(int page, int pageSize);
    Task<Booking?> GetByIdAsync(Guid id);
    Task AddAsync(Booking booking);
    Task UpdateAsync(Booking booking);
    Task<List<Booking>> GetExpiredPendingAsync(); // For Background Service
}