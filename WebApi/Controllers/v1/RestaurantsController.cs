using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/restaurants")]
public class RestaurantsController : ControllerBase
{
    private readonly IRestaurantService _svc;
    public RestaurantsController(IRestaurantService svc) => _svc = svc;

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

    [HttpGet("by-cuisine")]
    public async Task<IActionResult> GetByCuisine([FromQuery] string cuisineType, [FromQuery] int page = 1, [FromQuery] int pageSize = 6)
        => Ok(await _svc.GetByCuisineAsync(cuisineType, page, pageSize));

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateRestaurantDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.CreateAsync(dto);
        return result.Success ? Created($"api/v1/restaurants/{result.Data!.Id}", result) : BadRequest(result);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRestaurantDto dto)
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
