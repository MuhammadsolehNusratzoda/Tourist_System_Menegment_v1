using AutoMapper;

public class TransportService : ITransportService
{
    private readonly ITransportRepository _repo;
    private readonly IMapper _mapper;

    public TransportService(ITransportRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<ApiResponse<PagedResult<TransportDto>>> GetAllAsync(int page, int pageSize)
    {
        var result = await _repo.GetAllAsync(page, pageSize);
        return ApiResponse<PagedResult<TransportDto>>.Ok(new PagedResult<TransportDto>
        {
            Items = _mapper.Map<List<TransportDto>>(result.Items),
            TotalCount = result.TotalCount, Page = result.Page, PageSize = result.PageSize
        });
    }

    public async Task<ApiResponse<TransportDto>> GetByIdAsync(Guid id)
    {
        var t = await _repo.GetByIdAsync(id);
        if (t == null) return ApiResponse<TransportDto>.Fail("Transport not found.");
        return ApiResponse<TransportDto>.Ok(_mapper.Map<TransportDto>(t));
    }

    public async Task<ApiResponse<PagedResult<TransportDto>>> GetByTypeAsync(string transportType, int page, int pageSize)
    {
        var result = await _repo.GetByTypeAsync(transportType, page, pageSize);
        return ApiResponse<PagedResult<TransportDto>>.Ok(new PagedResult<TransportDto>
        {
            Items = _mapper.Map<List<TransportDto>>(result.Items),
            TotalCount = result.TotalCount, Page = result.Page, PageSize = result.PageSize
        });
    }

    public async Task<ApiResponse<List<TransportDto>>> GetAvailableAsync(DateTime date)
    {
        var result = await _repo.GetAvailableAsync(date);
        return ApiResponse<List<TransportDto>>.Ok(_mapper.Map<List<TransportDto>>(result));
    }

    public async Task<ApiResponse<TransportDto>> CreateAsync(CreateTransportDto dto)
    {
        var t = _mapper.Map<Transport>(dto);
        await _repo.AddAsync(t);
        return ApiResponse<TransportDto>.Ok(_mapper.Map<TransportDto>(t), "Transport created.");
    }

    public async Task<ApiResponse<TransportDto>> UpdateAsync(Guid id, UpdateTransportDto dto)
    {
        var t = await _repo.GetByIdAsync(id);
        if (t == null) return ApiResponse<TransportDto>.Fail("Transport not found.");
        _mapper.Map(dto, t);
        t.UpdatedAt = DateTime.UtcNow;
        await _repo.UpdateAsync(t);
        return ApiResponse<TransportDto>.Ok(_mapper.Map<TransportDto>(t), "Updated.");
    }

    public async Task<ApiResponse<bool>> DeleteAsync(Guid id)
    {
        var t = await _repo.GetByIdAsync(id);
        if (t == null) return ApiResponse<bool>.Fail("Transport not found.");
        await _repo.DeleteAsync(id);
        return ApiResponse<bool>.Ok(true, "Deleted.");
    }
}
