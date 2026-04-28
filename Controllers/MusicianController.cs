using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Musicana.Api.Models;
using Musicana.Api.Requests;
using Musicana.Api.Responses;
using Musicana.Api.Services;

namespace Musicana.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MusicianController : ControllerBase
{
    private readonly IMusicianService _service;
    private readonly IMusician_InstrumentService _musician_InstrumentService;
    private readonly IMusician_SongService _musician_SongService;
    public MusicianController(IMusicianService service, IMusician_SongService musician_SongService
    , IMusician_InstrumentService musician_InstrumentService)
    {
        _service = service;
        _musician_SongService = musician_SongService;
        _musician_InstrumentService = musician_InstrumentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMusiciansAsync()
    {
        try
        {
            var musicians = await _service.GetAllMusiciansAsync();
            return Ok(musicians);
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetMusicianByIdAsync(int id)
    {
        try
        {
            var musician = await _service.GetMusicianByIdAsync(id);
            return Ok(musician);
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpGet("search")]
    public async Task<IActionResult> GetMusicianByNameAsync([FromQuery] string name)
    {
        try
        {
            var musicians = await _service.GetMusicianByNameAsync(name);
            return Ok(musicians);
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateMusicianAsync(CreateMusicianDto musicianDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            await _service.CreateMusicianAsync(musicianDto);
            return Ok("Musician created successfully");
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteMusicianAsync(int id)
    {
        try
        {
            await _service.DeleteMusicianAsync(id);
            return NoContent();
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateMusicianAsync(int id, [FromBody] EditMusicianDto musicianDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            await _service.UpdateMusicianAsync(id, musicianDto);
            return NoContent();
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("assign-song")]
    public async Task<IActionResult> AssignSongToMusicianAsync([FromBody] AssignSongDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _musician_SongService.AssignSongToMusicianAsync(dto);
            return Ok("Song assigned to musician successfully");
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("assign-instrument")]
    public async Task<IActionResult> AssignInstrumentToMusicianAsync([FromBody] AssignInstrumentDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            await _musician_InstrumentService.AssignInstrumentToMusicianAsync(dto);
            return Ok("Instrument assigned to musician successfully");
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
}