using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/v1/bookings")]
[Authorize]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _svc;
    public BookingsController(IBookingService svc) => _svc = svc;

    [HttpGet("my")]
    public async Task<IActionResult> GetMine([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        return Ok(await _svc.GetByUserIdAsync(userId, page, pageSize));
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        => Ok(await _svc.GetAllAsync(page, pageSize));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _svc.GetByIdAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookingDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _svc.CreateAsync(dto, userId);
        return result.Success ? Created($"api/v1/bookings/{result.Data!.Id}", result) : BadRequest(result);
    }

    [HttpPatch("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var role = User.FindFirstValue(ClaimTypes.Role) ?? "Tourist";
        var result = await _svc.CancelAsync(id, userId, role);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPatch("{id:guid}/status")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] string status)
    {
        var result = await _svc.UpdateStatusAsync(id, status);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
