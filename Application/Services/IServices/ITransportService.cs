public interface ITransportService
{
    Task<ApiResponse<PagedResult<TransportDto>>> GetAllAsync(int page, int pageSize);
    Task<ApiResponse<TransportDto>> GetByIdAsync(Guid id);
    Task<ApiResponse<PagedResult<TransportDto>>> GetByTypeAsync(string transportType, int page, int pageSize);
    Task<ApiResponse<List<TransportDto>>> GetAvailableAsync(DateTime date);
    Task<ApiResponse<TransportDto>> CreateAsync(CreateTransportDto dto);
    Task<ApiResponse<TransportDto>> UpdateAsync(Guid id, UpdateTransportDto dto);
    Task<ApiResponse<bool>> DeleteAsync(Guid id);
}