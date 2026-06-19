using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/transport")]
public class TransportController : ControllerBase
{
    private readonly ITransportService _svc;
    public TransportController(ITransportService svc) => _svc = svc;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 6)
        => Ok(await _svc.GetAllAsync(page, pageSize));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _svc.GetByIdAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpGet("by-type")]
    public async Task<IActionResult> GetByType([FromQuery] string type, [FromQuery] int page = 1, [FromQuery] int pageSize = 6)
        => Ok(await _svc.GetByTypeAsync(type, page, pageSize));

    [HttpGet("available")]
    public async Task<IActionResult> GetAvailable([FromQuery] DateTime date)
        => Ok(await _svc.GetAvailableAsync(date));

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateTransportDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.CreateAsync(dto);
        return result.Success ? Created($"api/v1/transport/{result.Data!.Id}", result) : BadRequest(result);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTransportDto dto)
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
