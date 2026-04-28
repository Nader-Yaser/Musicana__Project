using Microsoft.AspNetCore.Mvc;
using Musicana.Api.Services;
using Musicana.Api.Requests;
namespace Musicana.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
        public class AuthController : ControllerBase
        {
            private readonly IAuthService _authService;

            public AuthController(IAuthService authService)
            {
                _authService = authService;
            }

            [HttpPost("register")]
            public async Task<IActionResult> Register(RegisterRequest request)
            {
                var result = await _authService.RegisterAsync(request);

                if (!result.IsAuthenticated)
                    return BadRequest(result.Message);

                return Ok(result);
            }

            [HttpPost("login")]
            public async Task<IActionResult> Login(LoginRequest request)
            {
                var result = await _authService.LoginAsync(request);

                if (!result.IsAuthenticated)
                    return BadRequest(result.Message);

                return Ok(result);
            }
        }
    }

