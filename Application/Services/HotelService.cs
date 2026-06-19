using AutoMapper;

public class HotelService : IHotelService
{
    private readonly IHotelRepository _repo;
    private readonly IMapper _mapper;

    public HotelService(IHotelRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<ApiResponse<PagedResult<HotelDto>>> GetAllAsync(int page, int pageSize)
    {
        var result = await _repo.GetAllAsync(null, null, null, null, page, pageSize);
        return ApiResponse<PagedResult<HotelDto>>.Ok(new PagedResult<HotelDto>
        {
            Items = _mapper.Map<List<HotelDto>>(result.Items),
            TotalCount = result.TotalCount, Page = result.Page, PageSize = result.PageSize
        });
    }

    public async Task<ApiResponse<HotelDto>> GetByIdAsync(Guid id)
    {
        var hotel = await _repo.GetByIdAsync(id);
        if (hotel == null) return ApiResponse<HotelDto>.Fail("Hotel not found.");
        return ApiResponse<HotelDto>.Ok(_mapper.Map<HotelDto>(hotel));
    }

    public async Task<ApiResponse<PagedResult<HotelDto>>> GetByCityAsync(string city, int page, int pageSize)
    {
        var result = await _repo.GetAllAsync(city, null, null, null, page, pageSize);
        return ApiResponse<PagedResult<HotelDto>>.Ok(new PagedResult<HotelDto>
        {
            Items = _mapper.Map<List<HotelDto>>(result.Items),
            TotalCount = result.TotalCount, Page = result.Page, PageSize = result.PageSize
        });
    }

    public async Task<ApiResponse<PagedResult<HotelDto>>> GetByStarsAsync(int stars, int page, int pageSize)
    {
        var result = await _repo.GetAllAsync(null, null, null, stars, page, pageSize);
        return ApiResponse<PagedResult<HotelDto>>.Ok(new PagedResult<HotelDto>
        {
            Items = _mapper.Map<List<HotelDto>>(result.Items),
            TotalCount = result.TotalCount, Page = result.Page, PageSize = result.PageSize
        });
    }

    public async Task<ApiResponse<List<HotelDto>>> GetTopRatedAsync(int count)
    {
        var result = await _repo.GetAllAsync(null, null, null, null, 1, count);
        return ApiResponse<List<HotelDto>>.Ok(_mapper.Map<List<HotelDto>>(result.Items));
    }

    public async Task<ApiResponse<HotelDto>> CreateAsync(CreateHotelDto dto)
    {
        var hotel = _mapper.Map<Hotel>(dto);
        await _repo.AddAsync(hotel);
        return ApiResponse<HotelDto>.Ok(_mapper.Map<HotelDto>(hotel), "Hotel created.");
    }

    public async Task<ApiResponse<HotelDto>> UpdateAsync(Guid id, UpdateHotelDto dto)
    {
        var hotel = await _repo.GetByIdAsync(id);
        if (hotel == null) return ApiResponse<HotelDto>.Fail("Hotel not found.");
        _mapper.Map(dto, hotel);
        hotel.UpdatedAt = DateTime.UtcNow;
        await _repo.UpdateAsync(hotel);
        return ApiResponse<HotelDto>.Ok(_mapper.Map<HotelDto>(hotel), "Updated.");
    }

    public async Task<ApiResponse<bool>> DeleteAsync(Guid id)
    {
        var hotel = await _repo.GetByIdAsync(id);
        if (hotel == null) return ApiResponse<bool>.Fail("Hotel not found.");
        await _repo.DeleteAsync(id);
        return ApiResponse<bool>.Ok(true, "Deleted.");
    }
}
