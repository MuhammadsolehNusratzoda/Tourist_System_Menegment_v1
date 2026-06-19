public interface IGuideService
{
    Task<ApiResponse<PagedResult<GuideDto>>> GetAllAsync(int page, int pageSize);
    Task<ApiResponse<GuideDto>> GetByIdAsync(Guid id);
    Task<ApiResponse<PagedResult<GuideDto>>> GetByLanguageAsync(string language, int page, int pageSize);
    Task<ApiResponse<List<GuideDto>>> GetTopRatedAsync(int count);
    Task<ApiResponse<GuideDto>> CreateAsync(CreateGuideDto dto);
    Task<ApiResponse<GuideDto>> UpdateAsync(Guid id, UpdateGuideDto dto);
    Task<ApiResponse<bool>> DeleteAsync(Guid id);
}