
public class GuideService : IGuidService
{
    private readonly AppDbContext _context;

    public GuideService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<PagedResult<GuideDto>>> GetAllAsync(int page, int pageSize)
    {
        var query = _context.Guides.AsQueryable();

        var total = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(g => new GuideDto
            {
                Id = g.Id,
                Name = g.Name,
                Languages = g.Languages,
                Rating = g.Rating
            })
            .ToListAsync();

        return ApiResponse<PagedResult<GuideDto>>.Success(new PagedResult<GuideDto>
        {
            Items = items,
            TotalCount = total,
            Page = page,
            PageSize = pageSize
        });
    }

    public async Task<ApiResponse<GuideDto>> GetByIdAsync(Guid id)
    {
        var guide = await _context.Guides.FindAsync(id);

        if (guide == null)
            return ApiResponse<GuideDto>.Failure("Guide not found");

        return ApiResponse<GuideDto>.Success(new GuideDto
        {
            Id = guide.Id,
            Name = guide.Name,
            Languages = guide.Languages,
            Rating = guide.Rating
        });
    }

    public async Task<ApiResponse<PagedResult<GuideDto>>> GetByLanguageAsync(string language, int page, int pageSize)
    {
        var query = _context.Guides.Where(g => g.Languages.Contains(language));

        var total = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(g => new GuideDto
            {
                Id = g.Id,
                Name = g.Name,
                Languages = g.Languages,
                Rating = g.Rating
            })
            .ToListAsync();

        return ApiResponse<PagedResult<GuideDto>>.Success(new PagedResult<GuideDto>
        {
            Items = items,
            TotalCount = total,
            Page = page,
            PageSize = pageSize
        });
    }

    public async Task<ApiResponse<List<GuideDto>>> GetTopRatedAsync(int count)
    {
        var guides = await _context.Guides
            .OrderByDescending(g => g.Rating)
            .Take(count)
            .Select(g => new GuideDto
            {
                Id = g.Id,
                Name = g.Name,
                Languages = g.Languages,
                Rating = g.Rating
            })
            .ToListAsync();

        return ApiResponse<List<GuideDto>>.Success(guides);
    }

    public async Task<ApiResponse<GuideDto>> CreateAsync(CreateGuideDto dto)
    {
        var guide = new Guide
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Languages = dto.Languages,
            Rating = dto.Rating
        };

        _context.Guides.Add(guide);
        await _context.SaveChangesAsync();

        return await GetByIdAsync(guide.Id);
    }

    public async Task<ApiResponse<GuideDto>> UpdateAsync(Guid id, UpdateGuideDto dto)
    {
        var guide = await _context.Guides.FindAsync(id);

        if (guide == null)
            return ApiResponse<GuideDto>.Failure("Guide not found");

        guide.Name = dto.Name;
        guide.Languages = dto.Languages ?? guide.Languages;
        guide.Rating = dto.Rating;

        await _context.SaveChangesAsync();

        return await GetByIdAsync(id);
    }

    public async Task<ApiResponse<bool>> DeleteAsync(Guid id)
    {
        var guide = await _context.Guides.FindAsync(id);

        if (guide == null)
            return ApiResponse<bool>.Failure("Guide not found");

        _context.Guides.Remove(guide);
        await _context.SaveChangesAsync();

        return ApiResponse<bool>.Success(true);
    }
}