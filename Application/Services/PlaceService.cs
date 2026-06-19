using AutoMapper;

public class PlaceService : IPlaceService
{
    private readonly IPlaceRepository _repo;
    private readonly IMapper _mapper;

    public PlaceService(IPlaceRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<ApiResponse<PagedResult<PlaceDto>>> GetAllAsync(
        string? location, int page, int pageSize)
    {
        var result = await _repo.GetAllAsync(location, page, pageSize);
        var dtoResult = new PagedResult<PlaceDto>
        {
            Items = _mapper.Map<List<PlaceDto>>(result.Items),
            TotalCount = result.TotalCount,
            Page = result.Page,
            PageSize = result.PageSize
        };
        return ApiResponse<PagedResult<PlaceDto>>.Ok(dtoResult);
    }

    public async Task<ApiResponse<PlaceDto>> GetByIdAsync(Guid id)
    {
        var place = await _repo.GetByIdAsync(id);
        if (place == null)
            return ApiResponse<PlaceDto>.Fail("Place not found.");
        return ApiResponse<PlaceDto>.Ok(_mapper.Map<PlaceDto>(place));
    }

    public async Task<ApiResponse<PlaceDto>> CreateAsync(CreatePlaceDto dto, Guid userId)
    {
        var place = _mapper.Map<Place>(dto);
        place.CreatedByUserId = userId;
        await _repo.AddAsync(place);
        return ApiResponse<PlaceDto>.Ok(_mapper.Map<PlaceDto>(place), "Place created.");
    }

    public async Task<ApiResponse<PlaceDto>> UpdateAsync(
        Guid id, UpdatePlaceDto dto, Guid userId, string role)
    {
        var place = await _repo.GetByIdAsync(id);
        if (place == null)
            return ApiResponse<PlaceDto>.Fail("Place not found.");

        // Only admin or creator can update
        if (role != "Admin" && place.CreatedByUserId != userId)
            return ApiResponse<PlaceDto>.Fail("Access denied.");

        _mapper.Map(dto, place);
        await _repo.UpdateAsync(place);
        return ApiResponse<PlaceDto>.Ok(_mapper.Map<PlaceDto>(place), "Updated.");
    }

    public async Task<ApiResponse<bool>> DeleteAsync(Guid id, Guid userId, string role)
    {
        var place = await _repo.GetByIdAsync(id);
        if (place == null)
            return ApiResponse<bool>.Fail("Place not found.");

        if (role != "Admin" && place.CreatedByUserId != userId)
            return ApiResponse<bool>.Fail("Access denied.");

        await _repo.DeleteAsync(id);
        return ApiResponse<bool>.Ok(true, "Deleted.");
    }
}