using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Musicana.Api.Enums;
using Musicana.Api.Requests;
using Musicana.Api.Services;

namespace Musicana.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SongController : ControllerBase
{
    private readonly ISongService _service;

    public SongController(ISongService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetSongsAsunc()
    {
        try
        {
            var songs = await _service.GetSongsAsync();
            return Ok(songs);
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpGet("{id::int}")]
    public async Task<IActionResult> GetSongByIdAsync(int id)
    {
        try
        {
            var song = await _service.GetSongByIdAsync(id);
            return Ok(song);
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchSongsAsync(
        [FromQuery] SongGenres? genre,
        [FromQuery] string? title)
    {
        try
        {
            if (genre.HasValue && Enum.IsDefined(typeof(SongGenres), genre.Value))
            {
                var songs = await _service.GetSongsByGenreAsync(genre.Value);
                return Ok(songs);
            }

            if (!string.IsNullOrWhiteSpace(title))
            {
                var songs = await _service.GetSongsByTitleAsync(title);
                return Ok(songs);
            }
            var allSongs = await _service.GetSongsAsync();
            return Ok(allSongs);
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateSongAsync([FromForm] CreateSongDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _service.CreateSongAsync(dto);
            return Ok("Song Added Successfully");
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id::int}")]
    public async Task<IActionResult> EditSongAsync(int id, [FromForm] EditSongDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        try
        {
            await _service.UpdateSongAsync(id, dto);
            return NoContent();
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteSongAsync(int id)
    {
        try
        {
            await _service.DeleteSongAsync(id);
            return NoContent();
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

}