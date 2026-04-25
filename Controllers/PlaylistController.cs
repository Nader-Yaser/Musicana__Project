using Microsoft.AspNetCore.Mvc;
using Musicana.Api.Requests;
using Musicana.Api.Services;

namespace Musicana.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlaylistController : ControllerBase
{
    private readonly IPlaylistService _service;

    public PlaylistController(IPlaylistService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetPlaylistsAsync()
    {
        try
        {
            var playlists = await _service.GetPlaylistsAsync();
            return Ok(playlists);
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPlaylistByIdAsync(int id)
    {
        try
        {
            var playlist = await _service.GetPlaylistByIdAsync(id);
            return Ok(playlist);
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchPlaylistsAsync([FromQuery] string? name)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                var playlists = await _service.GetPlaylistsByNameAsync(name);
                return Ok(playlists);
            }
            var allPlaylists = await _service.GetPlaylistsAsync();
            return Ok(allPlaylists);
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpPost]
    public async Task<IActionResult> CreatePlaylistAsync([FromForm] CreatePlaylistDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _service.CreatePlaylistAsync(dto);
            return Ok("Playlist Created Successfully");
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> EditPlaylistAsync(int id, [FromForm] EditPlaylistDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            await _service.UpdatePlaylistAsync(id, dto);
            return NoContent();
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePlaylistAsync(int id)
    {
        try
        {
            await _service.DeletePlaylistAsync(id);
            return NoContent();
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpPost("{playlistId:int}/songs/{songId:int}")]
    public async Task<IActionResult> AddSongToPlaylistAsync(int playlistId, int songId)
    {
        try
        {
            await _service.AddSongToPlaylistAsync(playlistId, songId);
            return Ok("Song added to playlist successfully");
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpDelete("{playlistId:int}/songs/{songId:int}")]
    public async Task<IActionResult> RemoveSongFromPlaylistAsync(int playlistId, int songId)
    {
        try
        {
            await _service.RemoveSongFromPlaylistAsync(playlistId, songId);
            return Ok("Song removed from playlist successfully");
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
}
