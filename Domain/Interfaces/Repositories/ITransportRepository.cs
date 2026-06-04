public interface ITransportRepository
{
    Task<PagedResult<Transport>> GetAllAsync(int page, int pageSize);
    Task<Transport?> GetByIdAsync(Guid id);
    Task AddAsync(Transport transport);
    Task UpdateAsync(Transport transport);
    Task DeleteAsync(Guid id);
    Task<PagedResult<Transport>> GetByTypeAsync(string transportType, int page, int pageSize);
    Task<List<Transport>> GetAvailableAsync(DateTime date); // For Background Service
}