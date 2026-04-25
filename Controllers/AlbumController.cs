using Microsoft.AspNetCore.Mvc;
using Musicana.Api.Requests;
using Musicana.Api.Services;

namespace Musicana.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlbumController : ControllerBase
{
    private readonly IAlbumService _service;

    public AlbumController(IAlbumService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAlbumsAsync()
    {
        try
        {
            var albums = await _service.GetAlbumsAsync();
            return Ok(albums);
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAlbumByIdAsync(int id)
    {
        try
        {
            var album = await _service.GetAlbumByIdAsync(id);
            return Ok(album);
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchAlbumsAsync([FromQuery] string? title)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                var albums = await _service.GetAlbumsByTitleAsync(title);
                return Ok(albums);
            }
            var allAlbums = await _service.GetAlbumsAsync();
            return Ok(allAlbums);
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAlbumAsync([FromForm] CreateAlbumDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _service.CreateAlbumAsync(dto);
            return Ok("Album Added Successfully");
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> EditAlbumAsync(int id, [FromForm] EditAlbumDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            await _service.UpdateAlbumAsync(id, dto);
            return NoContent();
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAlbumAsync(int id)
    {
        try
        {
            await _service.DeleteAlbumAsync(id);
            return NoContent();
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpPost("{albumId:int}/songs/{songId:int}")]
    public async Task<IActionResult> AssignSongToAlbumAsync(int albumId, int songId)
    {
        try
        {
            await _service.AssignSongToAlbumAsync(albumId, songId);
            return Ok("Song assigned to album successfully");
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpDelete("songs/{songId:int}")]
    public async Task<IActionResult> RemoveSongFromAlbumAsync(int songId)
    {
        try
        {
            await _service.RemoveSongFromAlbumAsync(songId);
            return Ok("Song removed from album successfully");
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
}
