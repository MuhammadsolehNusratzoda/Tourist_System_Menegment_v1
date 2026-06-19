using AutoMapper;
using Microsoft.EntityFrameworkCore;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _repo;
    private readonly IMapper _mapper;
    private readonly AppDbContext _ctx;

    public ReviewService(IReviewRepository repo, IMapper mapper, AppDbContext ctx)
    {
        _repo = repo;
        _mapper = mapper;
        _ctx = ctx;
    }

    public async Task<ApiResponse<PagedResult<ReviewDto>>> GetAllAsync(int page, int pageSize)
    {
        var all = await _ctx.Reviews.OrderByDescending(r => r.CreatedAt)
            .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        var total = await _ctx.Reviews.CountAsync();
        return ApiResponse<PagedResult<ReviewDto>>.Ok(new PagedResult<ReviewDto>
        {
            Items = _mapper.Map<List<ReviewDto>>(all), TotalCount = total, Page = page, PageSize = pageSize
        });
    }

    public async Task<ApiResponse<ReviewDto>> GetByIdAsync(Guid id)
    {
        var r = await _ctx.Reviews.FindAsync(id);
        if (r == null) return ApiResponse<ReviewDto>.Fail("Review not found.");
        return ApiResponse<ReviewDto>.Ok(_mapper.Map<ReviewDto>(r));
    }

    public async Task<ApiResponse<PagedResult<ReviewDto>>> GetByUserIdAsync(Guid userId, int page, int pageSize)
    {
        var query = _ctx.Reviews.Where(r => r.UserId == userId).OrderByDescending(r => r.CreatedAt);
        var total = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return ApiResponse<PagedResult<ReviewDto>>.Ok(new PagedResult<ReviewDto>
        {
            Items = _mapper.Map<List<ReviewDto>>(items), TotalCount = total, Page = page, PageSize = pageSize
        });
    }

    public async Task<ApiResponse<PagedResult<ReviewDto>>> GetByEntityAsync(Guid entityId, string entityType, int page, int pageSize)
    {
        var all = await _repo.GetByReferenceAsync(entityType, entityId);
        var paged = all.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        return ApiResponse<PagedResult<ReviewDto>>.Ok(new PagedResult<ReviewDto>
        {
            Items = _mapper.Map<List<ReviewDto>>(paged), TotalCount = all.Count, Page = page, PageSize = pageSize
        });
    }

    public async Task<ApiResponse<double>> GetAverageRatingAsync(Guid entityId, string entityType)
    {
        var reviews = await _repo.GetByReferenceAsync(entityType, entityId);
        if (!reviews.Any()) return ApiResponse<double>.Ok(0);
        return ApiResponse<double>.Ok(reviews.Average(r => r.Rating));
    }

    public async Task<ApiResponse<ReviewDto>> CreateAsync(CreateReviewDto dto)
    {
        var review = _mapper.Map<Review>(dto);
        await _repo.AddAsync(review);
        return ApiResponse<ReviewDto>.Ok(_mapper.Map<ReviewDto>(review), "Review posted.");
    }

    public async Task<ApiResponse<ReviewDto>> UpdateAsync(Guid id)
    {
        var r = await _ctx.Reviews.FindAsync(id);
        if (r == null) return ApiResponse<ReviewDto>.Fail("Review not found.");
        return ApiResponse<ReviewDto>.Ok(_mapper.Map<ReviewDto>(r));
    }

    public async Task<ApiResponse<bool>> DeleteAsync(Guid id)
    {
        var r = await _ctx.Reviews.FindAsync(id);
        if (r == null) return ApiResponse<bool>.Fail("Review not found.");
        await _repo.DeleteAsync(id);
        return ApiResponse<bool>.Ok(true, "Deleted.");
    }
}
