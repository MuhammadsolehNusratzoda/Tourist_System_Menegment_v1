
using Microsoft.EntityFrameworkCore;

public class GuideService : IGuideService
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
                Rating = g.Rating,
                PricePerDay = g.PricePerDay,
                ExperienceYears = g.ExperienceYears,
                Bio = g.Bio,
                ImageUrl = g.ImageUrl
            })
            .ToListAsync();

        return ApiResponse<PagedResult<GuideDto>>.Ok(new PagedResult<GuideDto>
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
            return ApiResponse<GuideDto>.Fail("Guide not found");

        return ApiResponse<GuideDto>.Ok(new GuideDto
        {
            Id = guide.Id,
            Name = guide.Name,
            Languages = guide.Languages,
            Rating = guide.Rating,
            PricePerDay = guide.PricePerDay,
            ExperienceYears = guide.ExperienceYears,
            Bio = guide.Bio,
            ImageUrl = guide.ImageUrl
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
                Rating = g.Rating,
                PricePerDay = g.PricePerDay,
                ExperienceYears = g.ExperienceYears,
                Bio = g.Bio,
                ImageUrl = g.ImageUrl
            })
            .ToListAsync();

        return ApiResponse<PagedResult<GuideDto>>.Ok(new PagedResult<GuideDto>
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
                Rating = g.Rating,
                PricePerDay = g.PricePerDay,
                ExperienceYears = g.ExperienceYears,
                Bio = g.Bio,
                ImageUrl = g.ImageUrl
            })
            .ToListAsync();

        return ApiResponse<List<GuideDto>>.Ok(guides);
    }

    public async Task<ApiResponse<GuideDto>> CreateAsync(CreateGuideDto dto)
    {
        var guide = new Guide
        {
            Id = Guid.NewGuid(),
            UserId = dto.UserId,
            Name = dto.Name,
            Bio = dto.Bio,
            Languages = dto.Languages,
            PricePerDay = dto.PricePerDay,
            ExperienceYears = dto.ExperienceYears,
            Rating = 0,
            ImageUrl = dto.ImageUrl
        };

        _context.Guides.Add(guide);
        await _context.SaveChangesAsync();

        return await GetByIdAsync(guide.Id);
    }

    public async Task<ApiResponse<GuideDto>> UpdateAsync(Guid id, UpdateGuideDto dto)
    {
        var guide = await _context.Guides.FindAsync(id);

        if (guide == null)
            return ApiResponse<GuideDto>.Fail("Guide not found");

        guide.Name = dto.Name ?? guide.Name;
        guide.Bio = dto.Bio ?? guide.Bio;
        guide.Languages = dto.Languages ?? guide.Languages;
        guide.PricePerDay = dto.PricePerDay ?? guide.PricePerDay;
        guide.ExperienceYears = dto.ExperienceYears ?? guide.ExperienceYears;
        guide.ImageUrl = dto.ImageUrl ?? guide.ImageUrl;

        await _context.SaveChangesAsync();

        return await GetByIdAsync(id);
    }

    public async Task<ApiResponse<bool>> DeleteAsync(Guid id)
    {
        var guide = await _context.Guides.FindAsync(id);

        if (guide == null)
            return ApiResponse<bool>.Fail("Guide not found");

        _context.Guides.Remove(guide);
        await _context.SaveChangesAsync();

        return ApiResponse<bool>.Ok(true);
    }
}