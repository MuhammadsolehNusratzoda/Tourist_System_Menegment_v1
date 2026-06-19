using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/v1/places")]
public class PlacesController : ControllerBase
{
    private readonly IPlaceService _svc;
    public PlacesController(IPlaceService svc) => _svc = svc;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? location, [FromQuery] int page = 1, [FromQuery] int pageSize = 6)
        => Ok(await _svc.GetAllAsync(location, page, pageSize));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _svc.GetByIdAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreatePlaceDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _svc.CreateAsync(dto, userId);
        return result.Success ? Created($"api/v1/places/{result.Data!.Id}", result) : BadRequest(result);
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePlaceDto dto)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var role = User.FindFirstValue(ClaimTypes.Role) ?? "Tourist";
        var result = await _svc.UpdateAsync(id, dto, userId, role);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var role = User.FindFirstValue(ClaimTypes.Role) ?? "Tourist";
        var result = await _svc.DeleteAsync(id, userId, role);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
