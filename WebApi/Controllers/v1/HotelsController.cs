using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/hotels")]
public class HotelsController : ControllerBase
{
    private readonly IHotelService _svc;
    public HotelsController(IHotelService svc) => _svc = svc;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 6)
        => Ok(await _svc.GetAllAsync(page, pageSize));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _svc.GetByIdAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpGet("by-city")]
    public async Task<IActionResult> GetByCity([FromQuery] string city, [FromQuery] int page = 1, [FromQuery] int pageSize = 6)
        => Ok(await _svc.GetByCityAsync(city, page, pageSize));

    [HttpGet("by-stars")]
    public async Task<IActionResult> GetByStars([FromQuery] int stars, [FromQuery] int page = 1, [FromQuery] int pageSize = 6)
        => Ok(await _svc.GetByStarsAsync(stars, page, pageSize));

    [HttpGet("top-rated")]
    public async Task<IActionResult> GetTopRated([FromQuery] int count = 3)
        => Ok(await _svc.GetTopRatedAsync(count));

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateHotelDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.CreateAsync(dto);
        return result.Success ? Created($"api/v1/hotels/{result.Data!.Id}", result) : BadRequest(result);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateHotelDto dto)
    {
        var result = await _svc.UpdateAsync(id, dto);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _svc.DeleteAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }
}
