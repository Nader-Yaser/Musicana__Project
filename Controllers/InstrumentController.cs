using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Musicana.Api.Enums;
using Musicana.Api.Requests;
using Musicana.Api.Services;

namespace Musicana.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class InstrumentsController : ControllerBase
{
    private readonly I_InstrumentService _service;

    public InstrumentsController(I_InstrumentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllInstrumentsAsync()
    {
        try
        {
            var instruments = await _service.GetAllInstrumentsAsync();
            return Ok(instruments);
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetInstrumentByIdAsync(int id)
    {
        try
        {
            var instrument = await _service.GetInstrumentByIdAsync(id);
            return Ok(instrument);
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpGet("search")]
    public async Task<IActionResult> GetInstrumentsByNameAsync([FromQuery] string name)
    {
        try
        {
            var instruments = await _service.GetInstrumentsByNameAsync(name);
            return Ok(instruments);
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpGet("type")]
    public async Task<IActionResult> GetInstrumentsByTypeAsync([FromQuery] InstrumentType type)
    {
        try
        {
            var instruments = await _service.GetInstrumentsByTypeAsync(type);
            return Ok(instruments);
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateInstrumentAsync([FromForm] CreateInstrumentDto instrumentDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            await _service.CreateInstrumentAsync(instrumentDto);
            return Ok("Instrument created successfully");
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateInstrumentAsync(int id, [FromForm] EditInstrumentDto instrumentDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            await _service.UpdateInstrumentAsync(id, instrumentDto);
            return NoContent();
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteInstrumentAsync(int id)
    {
        try
        {
            await _service.DeleteInstrumentAsync(id);
            return NoContent();
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
}