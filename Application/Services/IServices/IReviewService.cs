public interface IReviewService
{
    Task<ApiResponse<PagedResult<ReviewDto>>> GetAllAsync(int page, int pageSize);
    Task<ApiResponse<ReviewDto>> GetByIdAsync(Guid id);
    Task<ApiResponse<PagedResult<ReviewDto>>> GetByUserIdAsync(Guid userId, int page, int pageSize);
    Task<ApiResponse<PagedResult<ReviewDto>>> GetByEntityAsync(Guid entityId, string entityType, int page, int pageSize);
    Task<ApiResponse<double>> GetAverageRatingAsync(Guid entityId, string entityType); // For caching/background
    Task<ApiResponse<ReviewDto>> CreateAsync(CreateReviewDto dto);
    Task<ApiResponse<ReviewDto>> UpdateAsync(Guid id);
    Task<ApiResponse<bool>> DeleteAsync(Guid id);
}