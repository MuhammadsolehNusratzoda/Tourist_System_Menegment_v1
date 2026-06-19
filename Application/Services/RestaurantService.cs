using AutoMapper;

public class RestaurantService : IRestaurantService
{
    private readonly IRestaurantRepository _repo;
    private readonly IMapper _mapper;

    public RestaurantService(IRestaurantRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<ApiResponse<PagedResult<RestaurantDto>>> GetAllAsync(int page, int pageSize)
    {
        var result = await _repo.GetAllAsync(page, pageSize);
        return ApiResponse<PagedResult<RestaurantDto>>.Ok(new PagedResult<RestaurantDto>
        {
            Items = _mapper.Map<List<RestaurantDto>>(result.Items),
            TotalCount = result.TotalCount, Page = result.Page, PageSize = result.PageSize
        });
    }

    public async Task<ApiResponse<RestaurantDto>> GetByIdAsync(Guid id)
    {
        var r = await _repo.GetByIdAsync(id);
        if (r == null) return ApiResponse<RestaurantDto>.Fail("Restaurant not found.");
        return ApiResponse<RestaurantDto>.Ok(_mapper.Map<RestaurantDto>(r));
    }

    public async Task<ApiResponse<PagedResult<RestaurantDto>>> GetByCityAsync(string city, int page, int pageSize)
    {
        var result = await _repo.GetByCityAsync(city, page, pageSize);
        return ApiResponse<PagedResult<RestaurantDto>>.Ok(new PagedResult<RestaurantDto>
        {
            Items = _mapper.Map<List<RestaurantDto>>(result.Items),
            TotalCount = result.TotalCount, Page = result.Page, PageSize = result.PageSize
        });
    }

    public async Task<ApiResponse<PagedResult<RestaurantDto>>> GetByCuisineAsync(string cuisineType, int page, int pageSize)
    {
        var result = await _repo.GetByCuisineAsync(cuisineType, page, pageSize);
        return ApiResponse<PagedResult<RestaurantDto>>.Ok(new PagedResult<RestaurantDto>
        {
            Items = _mapper.Map<List<RestaurantDto>>(result.Items),
            TotalCount = result.TotalCount, Page = result.Page, PageSize = result.PageSize
        });
    }

    public async Task<ApiResponse<RestaurantDto>> CreateAsync(CreateRestaurantDto dto)
    {
        var r = _mapper.Map<Restaurant>(dto);
        await _repo.AddAsync(r);
        return ApiResponse<RestaurantDto>.Ok(_mapper.Map<RestaurantDto>(r), "Restaurant created.");
    }

    public async Task<ApiResponse<RestaurantDto>> UpdateAsync(Guid id, UpdateRestaurantDto dto)
    {
        var r = await _repo.GetByIdAsync(id);
        if (r == null) return ApiResponse<RestaurantDto>.Fail("Restaurant not found.");
        _mapper.Map(dto, r);
        r.UpdatedAt = DateTime.UtcNow;
        await _repo.UpdateAsync(r);
        return ApiResponse<RestaurantDto>.Ok(_mapper.Map<RestaurantDto>(r), "Updated.");
    }

    public async Task<ApiResponse<bool>> DeleteAsync(Guid id)
    {
        var r = await _repo.GetByIdAsync(id);
        if (r == null) return ApiResponse<bool>.Fail("Restaurant not found.");
        await _repo.DeleteAsync(id);
        return ApiResponse<bool>.Ok(true, "Deleted.");
    }
}
