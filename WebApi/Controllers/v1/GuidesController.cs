using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/guides")]
public class GuidesController : ControllerBase
{
    private readonly IGuideService _svc;
    public GuidesController(IGuideService svc) => _svc = svc;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 6)
        => Ok(await _svc.GetAllAsync(page, pageSize));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _svc.GetByIdAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpGet("by-language")]
    public async Task<IActionResult> GetByLanguage([FromQuery] string language, [FromQuery] int page = 1, [FromQuery] int pageSize = 6)
        => Ok(await _svc.GetByLanguageAsync(language, page, pageSize));

    [HttpGet("top-rated")]
    public async Task<IActionResult> GetTopRated([FromQuery] int count = 3)
        => Ok(await _svc.GetTopRatedAsync(count));

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateGuideDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.CreateAsync(dto);
        return result.Success ? Created($"api/v1/guides/{result.Data!.Id}", result) : BadRequest(result);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateGuideDto dto)
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
