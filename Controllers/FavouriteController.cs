using Microsoft.AspNetCore.Mvc;
using Musicana.Api.Services;

namespace Musicana.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavouriteController : ControllerBase
{
    private readonly IFavouriteService _service;

    public FavouriteController(IFavouriteService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetFavouritesAsync()
    {
        try
        {
            var favourites = await _service.GetAllFavouritesAsync();
            return Ok(favourites);
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpPost("{songId:int}")]
    public async Task<IActionResult> AddToFavouritesAsync(int songId)
    {
        try
        {
            await _service.AddToFavouritesAsync(songId);
            return Ok("Song added to favourites successfully");
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpDelete("{songId:int}")]
    public async Task<IActionResult> RemoveFromFavouritesAsync(int songId)
    {
        try
        {
            await _service.RemoveFromFavouritesAsync(songId);
            return Ok("Song removed from favourites successfully");
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpGet("{songId:int}/check")]
    public async Task<IActionResult> IsFavouriteAsync(int songId)
    {
        try
        {
            var isFavourite = await _service.IsFavouriteAsync(songId);
            return Ok(new { songId, isFavourite });
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
}
