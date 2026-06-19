using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/v1/reviews")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _svc;
    public ReviewsController(IReviewService svc) => _svc = svc;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        => Ok(await _svc.GetAllAsync(page, pageSize));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _svc.GetByIdAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpGet("by-entity")]
    public async Task<IActionResult> GetByEntity(
        [FromQuery] Guid entityId, [FromQuery] string entityType,
        [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        => Ok(await _svc.GetByEntityAsync(entityId, entityType, page, pageSize));

    [HttpGet("my")]
    [Authorize]
    public async Task<IActionResult> GetMine([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        return Ok(await _svc.GetByUserIdAsync(userId, page, pageSize));
    }

    [HttpGet("average-rating")]
    public async Task<IActionResult> GetAverageRating([FromQuery] Guid entityId, [FromQuery] string entityType)
        => Ok(await _svc.GetAverageRatingAsync(entityId, entityType));

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateReviewDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        dto.UserId = userId;
        var result = await _svc.CreateAsync(dto);
        return result.Success ? Created($"api/v1/reviews/{result.Data!.Id}", result) : BadRequest(result);
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _svc.DeleteAsync(id);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
